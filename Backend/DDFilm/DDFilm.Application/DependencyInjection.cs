using DDFilm.Application.Authentication.Commands.Register;
using DDFilm.Application.Behaviors;
using DDFilm.Domain.Factories.SessionMovies;
using DDFilm.Domain.Factories.Sessions;
using DDFilm.Domain.Policies;
using DDFilm.Domain.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DDFilm.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            { 
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<ISessionFactory, SessionFactory>();
            services.AddScoped<ISessionMovieFactory, SessionMovieFactory>();
            services.AddScoped<MovieDomainService>();
            services.AddScoped<SessionDomainService>();

            services.Scan(b => b.FromAssemblies(typeof(ISessionSettingsPolicy).Assembly)
                .AddClasses(c => c.AssignableTo<ISessionSettingsPolicy>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
