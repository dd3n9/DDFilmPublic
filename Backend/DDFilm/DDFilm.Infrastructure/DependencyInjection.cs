using DDFilm.Application.Common.Clients;
using DDFilm.Application.Common.Interfaces.Authentication;
using DDFilm.Application.Common.Interfaces.Caching;
using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Contracts.Configurations;
using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Repositories;
using DDFilm.Infrastructure.Authentication;
using DDFilm.Infrastructure.Common.Services;
using DDFilm.Infrastructure.Common.Services.Caching;
using DDFilm.Infrastructure.Common.Services.Clients;
using DDFilm.Infrastructure.EF;
using DDFilm.Infrastructure.EF.Context;
using DDFilm.Infrastructure.EF.Interceptors;
using DDFilm.Infrastructure.EF.Repositories;
using DDFilm.Infrastructure.EF.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DDFilm.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<ApplicationUserId>>()
                .AddEntityFrameworkStores<WriteDbContext>()
                .AddDefaultTokenProviders();

            services.AddMsSql(configuration)
                .AddAuth(configuration)
                .AddRedis(configuration);


            //DI Session
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ISessionReadService, SessionReadService>();
            
            //DI SessionMovie
            services.AddScoped<ISessionMovieReadService, SessionMovieReadService>();

            //DI SessionParticipant 
            services.AddScoped<ISessionParticipantReadService, SessionParticipantReadService>();

            //DI ApplicationUser
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IApplicationUserReadService, ApplicationUserReadService>();

            //DI Movie
            services.AddScoped<IMovieRepository, MovieRepository>();

            //DI MovieRating
            services.AddScoped<IMovieRatingReadService, MovieRatingReadService>();

            //DI Common Services
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IRatingNotifier, RatingNotifier>();

            //DI Interceptors
            services.AddScoped<PublishDomainEventsInterceptor>();

            //Configuration
            services.Configure<CookiesConfig>(configuration.GetSection(CookiesConfig.SectionName));

            return services;
        }

        private static IServiceCollection AddAuth(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var jwtSettings = new JwtConfig();
            configuration.Bind(JwtConfig.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }

        private static IServiceCollection AddRedis(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                var connection = configuration.GetConnectionString("Redis");
                options.InstanceName = "DDFilm_";
                options.Configuration = connection;
            });

            services.AddScoped<IRedisCacheService, RedisCacheService>();

            return services;
        } 
    }
}
