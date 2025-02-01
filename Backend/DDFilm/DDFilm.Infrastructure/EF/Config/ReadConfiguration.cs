using DDFilm.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDFilm.Infrastructure.EF.Config
{
    internal sealed class ReadConfiguration :
    IEntityTypeConfiguration<ApplicationUserReadModel>,
    IEntityTypeConfiguration<SessionReadModel>,
    IEntityTypeConfiguration<SessionParticipantReadModel>,
    IEntityTypeConfiguration<SessionMovieReadModel>,
    IEntityTypeConfiguration<MovieReadModel>,
    IEntityTypeConfiguration<MovieRatingReadModel>,
    IEntityTypeConfiguration<UserFriendReadModel>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserReadModel> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
        }

        public void Configure(EntityTypeBuilder<SessionReadModel> builder)
        {
            builder.ToTable("Sessions");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.SessionName).IsRequired();

            builder.HasMany(s => s.Participants)
                   .WithOne(p => p.Session)
                   .HasForeignKey(sp => sp.SessionId);

            builder.HasMany(s => s.SessionMovies)
                   .WithOne(s => s.Session)
                   .HasForeignKey(sm => sm.SessionId);

            builder.OwnsOne(s => s.Settings, sb =>
            {
                sb.ToTable("SessionSettings");

                sb.HasKey(s => s.Id);

                sb.WithOwner().HasForeignKey("SessionId");
            });
        }

        public void Configure(EntityTypeBuilder<MovieReadModel> builder)
        {
            builder.ToTable("Movies");
            builder.HasKey(m => m.Id);

            builder.HasMany(m => m.Ratings)
                .WithOne(sm => sm.Movie)
                .HasForeignKey(r => r.MovieId);
        }

        public void Configure(EntityTypeBuilder<UserFriendReadModel> builder)
        {
            builder.ToTable("UserFriends");
            builder.HasKey(uf => uf.Id);

            builder.HasOne<ApplicationUserReadModel>()
                   .WithMany()
                   .HasForeignKey(uf => uf.UserId);

            builder.HasOne<ApplicationUserReadModel>()
                   .WithMany()
                   .HasForeignKey(uf => uf.FriendId);
        }
        public void Configure(EntityTypeBuilder<SessionParticipantReadModel> builder)
        {
            builder.ToTable("SessionParticipants"); 
            builder.HasKey(sp => sp.Id);

            builder.HasOne(sp => sp.Session)
                   .WithMany(s => s.Participants)
                   .HasForeignKey(sp => sp.SessionId);

            builder.HasOne(sp => sp.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(sp => sp.UserId);

            builder.Property(sp => sp.Role).IsRequired();
        }

        public void Configure(EntityTypeBuilder<SessionMovieReadModel> builder)
        {
            builder.ToTable("SessionMovies");
            builder.HasKey(sp => sp.Id);

            builder.HasOne(sm => sm.Session)
                .WithMany(s => s.SessionMovies)
                .HasForeignKey(sm => sm.SessionId);

            builder.HasOne(sm => sm.Movie)
                .WithMany()
                .HasForeignKey(sm => sm.MovieId);

            builder.HasOne(sm => sm.ApplicationUser)
                .WithMany()
                .HasForeignKey(sm => sm.AddedByUserId);
        }

        public void Configure(EntityTypeBuilder<MovieRatingReadModel> builder)
        {
            builder.ToTable("MovieRatings");
            builder.HasKey(sp => sp.Id);

            builder.HasOne(mr => mr.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(mr => mr.MovieId);

            builder.HasOne(mr => mr.User)
                .WithMany()
                .HasForeignKey(mr => mr.UserId);

            builder.HasOne(mr => mr.Session)
                .WithMany()
                .HasForeignKey(mr => mr.SessionId);
        }
    }
}
