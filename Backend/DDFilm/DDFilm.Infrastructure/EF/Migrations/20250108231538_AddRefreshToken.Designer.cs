﻿// <auto-generated />
using System;
using DDFilm.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DDFilm.Infrastructure.EF.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20250108231538_AddRefreshToken")]
    partial class AddRefreshToken
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ddfilm")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DDFilm.Domain.ApplicationUserAggregate.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", "ddfilm");
                });

            modelBuilder.Entity("DDFilm.Domain.MovieAggregate.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TmdbId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies", "ddfilm");
                });

            modelBuilder.Entity("DDFilm.Domain.SessionAggregate.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("HashedPassword");

                    b.Property<string>("SessionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SessionName");

                    b.HasKey("Id");

                    b.ToTable("Sessions", "ddfilm");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", "ddfilm");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "ddfilm");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", "ddfilm");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", "ddfilm");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "ddfilm");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "ddfilm");
                });

            modelBuilder.Entity("DDFilm.Domain.ApplicationUserAggregate.ApplicationUser", b =>
                {
                    b.OwnsMany("DDFilm.Domain.ApplicationUserAggregate.Entities.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("AddedDate")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<DateTime>("CreatedAt")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<DateTime>("ExpiryDate")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<string>("JwtId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("Id");

                            b1.HasIndex("UserId");

                            b1.ToTable("RefreshToken", "ddfilm");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("DDFilm.Domain.ApplicationUserAggregate.Entities.UserFriend", "Friends", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<string>("FriendId")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("UserId")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("Id");

                            b1.HasIndex("FriendId");

                            b1.HasIndex("UserId");

                            b1.ToTable("UserFriend", "ddfilm");

                            b1.HasOne("DDFilm.Domain.ApplicationUserAggregate.ApplicationUser", null)
                                .WithMany()
                                .HasForeignKey("FriendId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Friends");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("DDFilm.Domain.MovieAggregate.Movie", b =>
                {
                    b.OwnsMany("DDFilm.Domain.MovieAggregate.Entities.MovieRating", "Ratings", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int?>("Rating")
                                .HasColumnType("int");

                            b1.Property<Guid?>("SessionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("UserId")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("Id");

                            b1.HasIndex("MovieId");

                            b1.HasIndex("SessionId");

                            b1.HasIndex("UserId");

                            b1.ToTable("MovieRatings", "ddfilm");

                            b1.WithOwner()
                                .HasForeignKey("MovieId");

                            b1.HasOne("DDFilm.Domain.SessionAggregate.Session", null)
                                .WithMany()
                                .HasForeignKey("SessionId");

                            b1.HasOne("DDFilm.Domain.ApplicationUserAggregate.ApplicationUser", null)
                                .WithMany()
                                .HasForeignKey("UserId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("DDFilm.Domain.SessionAggregate.Session", b =>
                {
                    b.OwnsMany("DDFilm.Domain.SessionAggregate.Entities.SessionMovie", "SessionMovies", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("AddedByUserId")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("CreatedAt")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<bool>("IsWatched")
                                .HasColumnType("bit");

                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("SessionId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("AddedByUserId");

                            b1.HasIndex("MovieId");

                            b1.HasIndex("SessionId");

                            b1.ToTable("SessionMovies", "ddfilm");

                            b1.HasOne("DDFilm.Domain.ApplicationUserAggregate.ApplicationUser", null)
                                .WithMany()
                                .HasForeignKey("AddedByUserId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.HasOne("DDFilm.Domain.MovieAggregate.Movie", null)
                                .WithMany()
                                .HasForeignKey("MovieId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("SessionId");
                        });

                    b.OwnsMany("DDFilm.Domain.SessionAggregate.Entities.SessionParticipant", "Participants", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("SessionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<string>("Role")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("UserId")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("Id", "SessionId");

                            b1.HasIndex("SessionId");

                            b1.HasIndex("UserId");

                            b1.ToTable("SessionParticipants", "ddfilm");

                            b1.WithOwner()
                                .HasForeignKey("SessionId");

                            b1.HasOne("DDFilm.Domain.ApplicationUserAggregate.ApplicationUser", null)
                                .WithMany()
                                .HasForeignKey("UserId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.OwnsOne("DDFilm.Domain.SessionAggregate.Entities.SessionSettings", "Settings", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime2")
                                .HasDefaultValueSql("GETUTCDATE()");

                            b1.Property<long>("ParticipantLimit")
                                .HasColumnType("bigint");

                            b1.Property<long>("RequiredMoviesPerUser")
                                .HasColumnType("bigint");

                            b1.Property<Guid>("SessionId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("SessionId")
                                .IsUnique();

                            b1.ToTable("SessionSettings", "ddfilm");

                            b1.WithOwner()
                                .HasForeignKey("SessionId");
                        });

                    b.Navigation("Participants");

                    b.Navigation("SessionMovies");

                    b.Navigation("Settings")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.HasOne("DDFilm.Domain.ApplicationUserAggregate.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.HasOne("DDFilm.Domain.ApplicationUserAggregate.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DDFilm.Domain.ApplicationUserAggregate.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<DDFilm.Domain.ApplicationUserAggregate.ValueObjects.ApplicationUserId>", b =>
                {
                    b.HasOne("DDFilm.Domain.ApplicationUserAggregate.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
