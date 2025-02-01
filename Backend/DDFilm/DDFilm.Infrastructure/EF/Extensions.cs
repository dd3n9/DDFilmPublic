using DDFilm.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDFilm.Infrastructure.EF
{
    internal static class Extensions
    {
        public static IServiceCollection AddMsSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReadDbContext>(ctx =>
            {
                ctx.UseSqlServer(configuration.GetConnectionString("DbConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddDbContext<WriteDbContext>(ctx =>
            {
                ctx.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            return services;
        }
    }
}
