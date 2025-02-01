using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.MovieAggregate;
using DDFilm.Domain.MovieAggregate.ValueObjects;
using DDFilm.Domain.SessionAggregate;
using DDFilm.Domain.SessionAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDFilm.Infrastructure.EF.Config
{
    public class WriteConfiguration : IEntityTypeConfiguration<Session>,
        IEntityTypeConfiguration<ApplicationUser>,
        IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            var sessionNameConverter = new ValueConverter<SessionName, string>(sn => sn.Value,
                sn => new SessionName(sn));

            var sessionPassword = new ValueConverter<HashedPassword, string>(sp => sp.Value, 
                sp => new HashedPassword(sp));

            builder.ToTable("Sessions");

            builder.HasKey(s => s.Id);

            builder
                .Property(s => s.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, id => SessionId.Create(id));

            builder
                .Property(s => s.Password)
                .HasConversion(sessionPassword)
                .HasColumnName("HashedPassword");

            builder
                .Property(s => s.SessionName)
                .HasConversion(sessionNameConverter)
                .HasColumnName("SessionName");

            builder
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            builder.OwnsOne(s => s.Settings, sb =>
            {
                sb.ToTable("SessionSettings");

                sb.HasKey(s => s.Id);

                sb.WithOwner().HasForeignKey("SessionId");

                sb.Property(r => r.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                sb.Property(s => s.Id)
                    .HasConversion(id => id.Value, value => new SessionSettingId(value));
            });


            builder.OwnsMany(s => s.Participants, pb =>
            {
                var converter = new ValueConverter<ApplicationUserId, string>(
                    id => id.Value,
                    value => new ApplicationUserId(value)
                );

                pb.ToTable("SessionParticipants");

                pb.WithOwner().HasForeignKey("SessionId");

                pb.HasKey("Id", "SessionId");

                pb.HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey(p => p.UserId);

                pb.Property(sp => sp.Id)
                    .HasConversion(id => id.Value, id => new SessionParticipantId(id));

                pb.Property(sp => sp.UserId)
                    .HasConversion(converter);

                pb.Property(sp => sp.Role)
                    .HasConversion(
                        role => role.ToString(),
                        value => (SessionRole)Enum.Parse(typeof(SessionRole), value)
                    );

                pb.Property(r => r.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();
            });

            builder.OwnsMany(s => s.SessionMovies, smb =>
            {
                var converter = new ValueConverter<ApplicationUserId, string>(
                id => id.Value,
                value => new ApplicationUserId(value)
                );

                smb.ToTable("SessionMovies");

                smb.HasKey(sm => sm.Id);
                smb.WithOwner().HasForeignKey("SessionId");

                smb.HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey(sm => sm.AddedByUserId);

                smb.Property(sm => sm.Id)
                    .HasConversion(id => id.Value, id => new SessionMovieId(id))
                    .ValueGeneratedNever();

                smb.Property(sm => sm.MovieId)
                    .HasConversion(id => id.Value, value => MovieId.Create(value));

                smb.Property(sm => sm.AddedByUserId)
                    .HasConversion(converter);

                smb.HasOne<Movie>()
                    .WithMany()
                    .HasForeignKey(sm => sm.MovieId);

                smb.Property(r => r.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();
            });

            builder.Metadata.FindNavigation(nameof(Session.SessionMovies))!
               .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var converter = new ValueConverter<ApplicationUserId, string>(
                id => id.Value,
                value => new ApplicationUserId(value)
                );

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasConversion(converter)
                .ValueGeneratedNever();

            builder
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            builder.OwnsMany(s => s.Friends, fb =>
            {
                fb.HasKey(s => s.Id);

                fb.Property(sp => sp.Id)
                    .HasConversion(id => id.Value, value => new UserFriendId(value));

                fb.WithOwner()
                    .HasForeignKey(uf => uf.UserId);

                fb.HasOne<ApplicationUser>()
                    .WithMany() 
                    .HasForeignKey(f => f.FriendId)
                    .OnDelete(DeleteBehavior.Restrict);

                fb.Property(r => r.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();
            });

            builder.OwnsMany(u => u.RefreshTokens, rtb =>
            {
                rtb.HasKey(u => u.Id);

                rtb.Property(u => u.Id)
                    .HasConversion(id => id.Value, id => new RefreshTokenId(id))
                    .ValueGeneratedNever();

                rtb.WithOwner()
                    .HasForeignKey("ApplicationUserId");

                rtb.Property(u => u.Token)
                    .HasConversion(t => t.Value, t => new Token(t));

                rtb.Property(u => u.JwtId)
                    .HasConversion(jwtId => jwtId.Value, jwtId => new JwtId(jwtId));

                rtb.Property(r => r.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();

                rtb.Property(r => r.AddedDate)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .ValueGeneratedOnAdd();

                rtb.Property(r => r.ExpiryDate)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .ValueGeneratedOnAdd();
            });
        } 

        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            var converter = new ValueConverter<ApplicationUserId, string>(
                id => id.Value,
                value => new ApplicationUserId(value)
                );

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).IsRequired();

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => MovieId.Create(value));

            builder.Property(m =>m.Title)
                .HasConversion(id => id.Value, value => new MovieTitle(value));

            builder
                .Property(m => m.TmdbId)
                .HasConversion(tmdbId => tmdbId.Value, tmdbId => new TmdbId(tmdbId));

            builder
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            builder.OwnsMany(m => m.Ratings, rb =>
            {
                rb.ToTable("MovieRatings");

                rb.HasKey(r => r.Id);
                rb.Property(r => r.Id)
                    .HasConversion(id => id.Value, value => new MovieRatingId(value));

                rb.Property(r => r.UserId)
                    .IsRequired();

                rb.Property(r => r.UserId)
                    .HasConversion(converter);

                rb.Property(r => r.SessionId)
                .HasConversion(
                    id => id.Value ,
                    value => new SessionId(value)
                )
                .IsRequired(false);


                rb.Property(r => r.Rating)
                    .HasConversion(rating => rating.Value, rating => new RatingValue(rating));

                rb.HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey(r => r.UserId);

                rb.HasOne<Session>()
                    .WithMany()
                    .HasForeignKey(r => r.SessionId);

                rb.Property(r => r.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()")
                    .ValueGeneratedOnAdd();
            });
        }
    }
}
