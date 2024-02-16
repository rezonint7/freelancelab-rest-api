using FluentValidation;
using Freelance.Application.Orders.Commands.CreateOrder;
using Freelance.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Freelance.Application {
    public static class DependencyInjection {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            });

            var validators = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
                .ToList();

            foreach (var validator in validators) {
                var validatorInterface = validator.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));

                if (validatorInterface != null) {
                    services.AddScoped(validatorInterface, validator);
                }
            }


            //services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(DependencyInjection)));
            //services.AddValidatorsFromAssemblies(new[] {Assembly.GetExecutingAssembly()});
            //services.AddValidatorsFromAssemblyContaining<CreateNewOrderCommandValidator>();
            return services;
        }
    }
}
