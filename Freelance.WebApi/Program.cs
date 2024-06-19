using Freelance.Persistence;
using Freelance.Application;
using Freelance.Application.Common.Mapping;
using Freelance.Application.Interfaces;
using Freelance.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Freelance.Persistence.Services;
using Freelance.Application.Common.Behaviors;
using System.Diagnostics;
using System.ComponentModel;
using Microsoft.OpenApi.Models;
using Serilog;
using FluentValidation;
using System.Globalization;
using Freelance.WebApi.Hubs;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Reflection;

string logsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "logs");
string logFilePath = Path.Combine(logsDirectory, "logs-.txt");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .MinimumLevel.Override("Warning", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
    .CreateLogger();
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("ru");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddPersistence(builder.Configuration);
builder.Host.UseSerilog();

builder.Services.AddCors(options => {
    options.AddPolicy("dev", policy => {
        policy.WithOrigins("http://localhost:3000") // Replace with your frontend domain
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddSignalR();
builder.Services.AddAutoMapper(config => {
    config.AddProfile(new AssemblyMappingProfile(typeof(Program).Assembly));
    config.AddProfile(new AssemblyMappingProfile(typeof(IFreelanceDBContext).Assembly));
});
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
})
.AddGitHub(options => {
    options.ClientId = builder.Configuration["OAuth:GitHub:ClientId"];
    options.ClientSecret = builder.Configuration["OAuth:GitHub:ClientSecret"];
    options.CallbackPath = new PathString(builder.Configuration["OAuth:GitHub:CallbackURL"]);
    options.AuthorizationEndpoint = builder.Configuration["OAuth:GitHub:AuthorizationEndpoint"];
    options.TokenEndpoint = builder.Configuration["OAuth:GitHub:TokenEndpoint"];
    options.UserInformationEndpoint = builder.Configuration["OAuth:GitHub:UserInfoEndpoint"];
    options.SaveTokens = true;

    options.Scope.Add("user");
    options.Scope.Add("user:email");
})
.AddGoogle(options => {
    options.ClientId = builder.Configuration["OAuth:Google:ClientId"];
    options.ClientSecret = builder.Configuration["OAuth:Google:ClientSecret"];
    options.CallbackPath = new PathString(builder.Configuration["OAuth:Google:CallbackURL"]);
    options.AuthorizationEndpoint = builder.Configuration["OAuth:Google:AuthorizationEndpoint"];
    options.TokenEndpoint = builder.Configuration["OAuth:Google:TokenEndpoint"];
    options.UserInformationEndpoint = builder.Configuration["OAuth:Google:UserInfoEndpoint"];
    options.SaveTokens = true;

    options.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
});

builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => {
	try {
		opt.SwaggerDoc("v1", new OpenApiInfo {
			Version = "v1",
			Title = "FreelanceLab API Docs",
			Contact = new OpenApiContact() {
				Name = "Kunavin Anton",
				Email = "rezonint@gmail.com",
			}
		});
		opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
			Name = "Authorization",
			Type = SecuritySchemeType.ApiKey,
			Scheme = "Bearer",
			BearerFormat = "JWT",
			In = ParameterLocation.Header,
			Description = "JSON Web Token based security",
		});
		opt.AddSecurityRequirement(new OpenApiSecurityRequirement {
			{
				new OpenApiSecurityScheme { Reference = new OpenApiReference {
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}}, new string[] {}
			}
		});

		opt.CustomSchemaIds(type => type.ToString());
		var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
		var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
		if (File.Exists(xmlPath)) {
			opt.IncludeXmlComments(xmlPath);
		}
	}
	catch (Exception ex) {
		Log.Error(ex, "Error configuring Swagger");
		throw;
	}
});
var app = builder.Build();

using(var scope = app.Services.CreateScope()) {
    var serviceProvider = scope.ServiceProvider;
	var context = serviceProvider.GetRequiredService<FreelanceDBContext>();
	DbInitializer.Initialize(context);
	try {
        
    }
    catch(Exception ex) {
        Log.Fatal(ex, "Fatal error");
    }
}

app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("dev");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();



app.MapHub<ChatHub>("/chathub").RequireCors("dev");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
