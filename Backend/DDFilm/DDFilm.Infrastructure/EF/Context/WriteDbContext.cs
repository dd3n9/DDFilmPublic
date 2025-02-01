using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.MovieAggregate;
using DDFilm.Domain.SessionAggregate;
using DDFilm.Infrastructure.EF.Config;
using DDFilm.Infrastructure.EF.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace DDFilm.Infrastructure.EF.Context
{
    public sealed class WriteDbContext : IdentityDbContext<ApplicationUser, IdentityRole<ApplicationUserId>, ApplicationUserId>
    {
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

        public WriteDbContext(DbContextOptions options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
        {
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("ddfilm")
                .Ignore<List<IDomainEvent>>();

            var configuration = new WriteConfiguration();
            modelBuilder.ApplyConfiguration<Session>(configuration);
            modelBuilder.ApplyConfiguration<ApplicationUser>(configuration);
            modelBuilder.ApplyConfiguration<Movie>(configuration);

            modelBuilder.Entity<ApplicationUser>(e =>
            {
                e.ToTable("Users");
            });

            var guidToStringConverter = new ValueConverter<ApplicationUserId, string>(
                id => id.Value,                   
                value => new ApplicationUserId(value)
            );

            modelBuilder.Entity<IdentityUserClaim<ApplicationUserId>>(builder =>
            {
                builder.Property(uc => uc.UserId)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<ApplicationUserId>>(builder =>
            {
                builder.Property(ul => ul.UserId)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityUserToken<ApplicationUserId>>(builder =>
            {
                builder.Property(ut => ut.UserId)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("UserTokens");
            });

            modelBuilder.Entity<IdentityRole<ApplicationUserId>>(builder =>
            {
                builder.Property(r => r.Id)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("Roles");
            });

            modelBuilder.Entity<IdentityRoleClaim<ApplicationUserId>>(builder =>
            {
                builder.Property(rc => rc.RoleId)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserRole<ApplicationUserId>>(builder =>
            {
                builder.Property(ur => ur.UserId)
                       .HasConversion(guidToStringConverter);

                builder.Property(ur => ur.RoleId)
                       .HasConversion(guidToStringConverter);

                builder.ToTable("UserRoles");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
