# Авторизация по OAuth Github
-Токены удалять не стал для того чтобы проверять было удобнее (по хорошему бы надо удалить) - appsettings.json
> Program.cs - настройки для авторизации:
``` C#
.AddOAuth("GitHub", options => {
    options.ClientId = builder.Configuration["OAuth:GitHub:ClientId"];
    options.ClientSecret = builder.Configuration["OAuth:GitHub:ClientSecret"];
    options.CallbackPath = new PathString(builder.Configuration["OAuth:GitHub:CallbackURL"]); 
    options.AuthorizationEndpoint = builder.Configuration["OAuth:GitHub:AuthorizationEndpoint"];
    options.TokenEndpoint = builder.Configuration["OAuth:GitHub:TokenEndpoint"];
    options.UserInformationEndpoint = builder.Configuration["OAuth:GitHub:UserInfoEndpoint"];
    options.SaveTokens = true;

    options.Scope.Add("user");
    options.Events = new OAuthEvents {
        OnCreatingTicket = async context => {
            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
            var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
            response.EnsureSuccessStatusCode();
            var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            context.RunClaimActions(json.RootElement);
        }
    };

```
>HomeController.cs - данные методы используются для перенаправления пользователя на регистрацию для получения кода и токена. GitHubOAuth (авторизация и получение кода в redirect_uri) OAuth (получение токена доступа)
``` C# 
[HttpGet("oauth/{provider}")]
public IActionResult GitHubOAuth(string provider) {
    var properties = new AuthenticationProperties {
        RedirectUri = "/oauth-handler/" + provider
    };
    return Challenge(properties, getProviderNormalized(provider));
}


[HttpGet("oauth-handler/{provider}")]
public async Task<IActionResult> OAuth(string provider) {

    var result = await HttpContext.AuthenticateAsync(getProviderNormalized(provider));
    if (result.Succeeded) {
        var tokens = result.Properties.GetTokens();
        ViewData["access_token"] = tokens.FirstOrDefault(t => t.Name == "access_token")?.Value;
        ViewData["provider"] = getProviderNormalized(provider);
        return View();
    }
    else {
        return NotFound("token");
    }
}

private string getProviderNormalized(string provider) { 
    switch (provider) {
        case "github": return "GitHub";
        case "google": return "Google";
        case "vkontakte": return "Vkontakte";
        default: return "null";
    }
}

```
>Промежуточная View для сохранения токена в localStorage и направления пользователя на форму регистрации
``` C# 
@{
    var accessToken = ViewData["access_token"];
    var provider = ViewData["provider"];
    ViewData["Title"] = "Redirect";
}
<script>
    localStorage.clear();
    localStorage.setItem('oauth_token', '{ "provider": @provider, "access_token": "@accessToken" }');
    window.location.href = 'https://localhost:3000/register';
</script>

```

> Команда для регистрации пользователя (OAuthProvider, OAuthToken, OAuthKey используются для добавления токенов в базу данных)
> производится после успешного получения токенов на стороне API, далее токены сохраняются в localStorage для получения их на стороне клиента, после чего получем данные и заполняем форму.
> По хорошему можно было сделать получение данных на стороне API и отдавать их на клиент React, но в любом случае токены пришлось бы отправлять обратно для регистрации (так как данные профиля Github не содержат Email, пользователю придется вводить его вручную, а Email является обязательным для регистрации)
``` C#
public class RegisterNewUserCommand: IRequest<Guid> {
    public string Login { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string Email { get; set; }

    public string? OAuthProvider { get; set; } = null;
    public string? OAuthToken { get; set; } = null;
    public string? OAuthKey { get; set; } = null;
}

```

> Обработчик команды для регистрации пользователя
``` C#
internal class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, Guid> {
    private readonly IAuthenticationService _authenticationService;

    public RegisterNewUserCommandHandler(IAuthenticationService authenticationService) {
        _authenticationService = authenticationService;
    }

    public async Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken) {
        if (request.Role.ToUpper() == "ADMIN" || request.Role.ToUpper() == "MANAGER") {
            throw new NotFoundException("Role", request.Role);
        }
        return await _authenticationService.RegisterUserAsync(request, cancellationToken);
    }
}
```

> Сервис авторизации. В методе RegisterUserAsync, если пользователь авторизуется через OAuth передаются токены, провайдер авторизации (например Github) и ключ (как я понял это id профиля на Github).
> Если же пользователь авторизуется не через OAuth, то добавление токенов в таблицы AspNetTokens и AspNetLogins не производится
``` C#
public class AuthenticationService : IAuthenticationService {
    private readonly FreelanceDBContext _freelanceDBContext;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UserService _userService;
    private readonly JwtService _jwtService;

    public AuthenticationService(
        UserService userService, 
        JwtService jwtService, 
        FreelanceDBContext freelanceDBContext,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager) {
        _freelanceDBContext = freelanceDBContext;
        _signInManager = signInManager;
        _userManager = userManager;
        _userService = userService;
        _jwtService = jwtService;
    }

    public async Task<string> Authenticate(string login, string password) {
        var result = await _signInManager.PasswordSignInAsync(login, password, false, lockoutOnFailure: false);

        if (!result.Succeeded) { throw new InvalidUserCredentialsException(); }

        var user = await _userManager.FindByNameAsync(login);
        return await _jwtService.GenerateJwtToken(user);
    }

    public async Task<Guid> RegisterUserAsync(RegisterNewUserCommand registerNewUserCommand, CancellationToken cancellationToken) {
        var userExist = await _userService.GetUserByLoginAsync(registerNewUserCommand.Login, cancellationToken);
        var role = await _freelanceDBContext.Roles.FirstOrDefaultAsync(role => role.NormalizedName == registerNewUserCommand.Role, cancellationToken);
        
        if (userExist != null) { throw new UserAlreadyExistsException(userExist.UserName); }
        if (role == null) { throw new NotFoundException(nameof(Order), registerNewUserCommand.Role); }

        var newUser = new ApplicationUser {
            UserName = registerNewUserCommand.Login,
            Email = registerNewUserCommand.Email,
            FirstName = registerNewUserCommand.FirstName,
            LastName = registerNewUserCommand.LastName,
            MiddleName = registerNewUserCommand.MiddleName,
            AvatarProfilePath = "",
            HeaderProfilePath = "",
            RegisterDate = DateTime.Now,
            About = ""
        };
        var result = await _userManager.CreateAsync(newUser, registerNewUserCommand.Password);
        if (!result.Succeeded) { throw new UserAlreadyExistsException(newUser.Email); } // исправить

        await _userManager.AddToRoleAsync(newUser, role.Name);

        var workExperience = await _freelanceDBContext.WorkExperience.FirstAsync();
        var category = await _freelanceDBContext.Categories.FirstAsync();
        if (role.NormalizedName == "IMPLEMENTER") {
            newUser.Implementers.Add(new Implementer {
                UserId = newUser.Id,
                Specialization = "",
                WorkExperience = workExperience,
                Skills = "",
                Category = category
            });
        }
        else if (role.NormalizedName == "CUSTOMER") {
            newUser.Customers.Add(new Customer {
                UserId = newUser.Id,
                IsTrusted = false,
            });
        }

        if (!string.IsNullOrEmpty(registerNewUserCommand.OAuthProvider) && !string.IsNullOrEmpty(registerNewUserCommand.OAuthToken)) {
            var login = new UserLoginInfo(registerNewUserCommand.OAuthProvider, registerNewUserCommand.OAuthKey, registerNewUserCommand.Login);
            await _userManager.AddLoginAsync(newUser, login);
            await _userManager.SetAuthenticationTokenAsync(newUser, registerNewUserCommand.OAuthProvider, "access_token", registerNewUserCommand.OAuthToken);
        }

        await _freelanceDBContext.SaveChangesAsync(cancellationToken);
        return newUser.Id;
    }

    public async Task<bool> Logout() {
        await _signInManager.SignOutAsync();
        return true;
    }

    public async Task<bool> ResetPassword(string login, string newPassword) {
        var user = await _userManager.FindByNameAsync(login);
        if(user == null) { throw new NotFoundException(nameof(ApplicationUser), login); }

        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
        return result.Succeeded;
    }
}
```
