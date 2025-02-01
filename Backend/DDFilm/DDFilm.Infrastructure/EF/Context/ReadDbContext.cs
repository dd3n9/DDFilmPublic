using DDFilm.Infrastructure.EF.Config;
using DDFilm.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace DDFilm.Infrastructure.EF.Context
{
    internal sealed class ReadDbContext : DbContext
    {

        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) 
        { 
        }

        public DbSet<SessionReadModel> Sessions { get; set; }
        public DbSet<ApplicationUserReadModel> Users { get; set; }
        public DbSet<MovieReadModel> Movies { get; set; }
                              
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ddfilm");

            var configuration = new ReadConfiguration();
            modelBuilder.ApplyConfiguration<ApplicationUserReadModel>(configuration);
            modelBuilder.ApplyConfiguration<SessionReadModel>(configuration);
            modelBuilder.ApplyConfiguration<SessionParticipantReadModel>(configuration);
            modelBuilder.ApplyConfiguration<SessionMovieReadModel>(configuration);
            modelBuilder.ApplyConfiguration<MovieReadModel>(configuration);
            modelBuilder.ApplyConfiguration<MovieRatingReadModel>(configuration);
            modelBuilder.ApplyConfiguration<UserFriendReadModel>(configuration);
        }
    }
}
