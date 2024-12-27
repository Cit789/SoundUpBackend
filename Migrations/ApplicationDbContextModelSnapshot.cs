﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SoundUp;

#nullable disable

namespace SoundUp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AlbumBaseUser", b =>
                {
                    b.Property<Guid>("FavoriteAlbumsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FavoritedUsersId")
                        .HasColumnType("uuid");

                    b.HasKey("FavoriteAlbumsId", "FavoritedUsersId");

                    b.HasIndex("FavoritedUsersId");

                    b.ToTable("AlbumBaseUser");
                });

            modelBuilder.Entity("ListenHistoryMusic", b =>
                {
                    b.Property<Guid>("ListenHistoriesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MusicHistoryId")
                        .HasColumnType("uuid");

                    b.HasKey("ListenHistoriesId", "MusicHistoryId");

                    b.HasIndex("MusicHistoryId");

                    b.ToTable("ListenHistoryMusic");
                });

            modelBuilder.Entity("SoundUp.Models.Album", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId", "Name")
                        .IsUnique();

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("SoundUp.Models.BaseUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ListenHistoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RefreshTokenId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AllUsers");

                    b.HasDiscriminator<string>("UserType").HasValue("BaseUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SoundUp.Models.ListenHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ListenHistories");
                });

            modelBuilder.Entity("SoundUp.Models.Music", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AlbumId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MusicAudioId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("AuthorId", "Name")
                        .IsUnique();

                    b.ToTable("Music");
                });

            modelBuilder.Entity("SoundUp.Models.MusicAudio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Audio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MusicId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("MusicId")
                        .IsUnique();

                    b.ToTable("MusicAudio");
                });

            modelBuilder.Entity("SoundUp.Models.Playlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("PlayLists");
                });

            modelBuilder.Entity("SoundUp.Models.PlaylistMusic", b =>
                {
                    b.Property<Guid>("PlaylistId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MusicId")
                        .HasColumnType("uuid");

                    b.HasKey("PlaylistId", "MusicId");

                    b.HasIndex("MusicId");

                    b.HasIndex("PlaylistId", "MusicId")
                        .IsUnique()
                        .HasDatabaseName("IX_PlaylistMusic_PlaylistId_MusicId");

                    b.ToTable("PlaylistMusic");
                });

            modelBuilder.Entity("SoundUp.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("boolean");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("SoundUp.Models.UserMusicFavorites", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MusicId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "MusicId");

                    b.HasIndex("MusicId");

                    b.HasIndex("UserId", "MusicId")
                        .IsUnique()
                        .HasDatabaseName("IX_UserMusicFavorite_UserId_MusicId");

                    b.ToTable("UserMusicFavorite");
                });

            modelBuilder.Entity("SoundUp.Models.UserPlaylists", b =>
                {
                    b.Property<Guid>("PlaylistId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("PlaylistId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPlaylists");
                });

            modelBuilder.Entity("SoundUp.Models.User", b =>
                {
                    b.HasBaseType("SoundUp.Models.BaseUser");

                    b.HasDiscriminator().HasValue("DefaultUser");
                });

            modelBuilder.Entity("SoundUp.Models.UserAuthor", b =>
                {
                    b.HasBaseType("SoundUp.Models.BaseUser");

                    b.HasDiscriminator().HasValue("AuthorForMusic");
                });

            modelBuilder.Entity("AlbumBaseUser", b =>
                {
                    b.HasOne("SoundUp.Models.Album", null)
                        .WithMany()
                        .HasForeignKey("FavoriteAlbumsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoundUp.Models.BaseUser", null)
                        .WithMany()
                        .HasForeignKey("FavoritedUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ListenHistoryMusic", b =>
                {
                    b.HasOne("SoundUp.Models.ListenHistory", null)
                        .WithMany()
                        .HasForeignKey("ListenHistoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoundUp.Models.Music", null)
                        .WithMany()
                        .HasForeignKey("MusicHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SoundUp.Models.Album", b =>
                {
                    b.HasOne("SoundUp.Models.UserAuthor", "Author")
                        .WithMany("Albums")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("SoundUp.Models.ListenHistory", b =>
                {
                    b.HasOne("SoundUp.Models.BaseUser", "User")
                        .WithOne("ListenHistory")
                        .HasForeignKey("SoundUp.Models.ListenHistory", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SoundUp.Models.Music", b =>
                {
                    b.HasOne("SoundUp.Models.Album", "Album")
                        .WithMany("AlbumMusic")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoundUp.Models.UserAuthor", "Author")
                        .WithMany("CreatedMusics")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("SoundUp.Models.MusicAudio", b =>
                {
                    b.HasOne("SoundUp.Models.Music", "Music")
                        .WithOne("MusicAudio")
                        .HasForeignKey("SoundUp.Models.MusicAudio", "MusicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Music");
                });

            modelBuilder.Entity("SoundUp.Models.Playlist", b =>
                {
                    b.HasOne("SoundUp.Models.BaseUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("SoundUp.Models.PlaylistMusic", b =>
                {
                    b.HasOne("SoundUp.Models.Music", "Music")
                        .WithMany("MusicInPlaylists")
                        .HasForeignKey("MusicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoundUp.Models.Playlist", "Playlist")
                        .WithMany("MusicList")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Music");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("SoundUp.Models.RefreshToken", b =>
                {
                    b.HasOne("SoundUp.Models.BaseUser", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("SoundUp.Models.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SoundUp.Models.UserMusicFavorites", b =>
                {
                    b.HasOne("SoundUp.Models.Music", "Music")
                        .WithMany("WhoFavorited")
                        .HasForeignKey("MusicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoundUp.Models.BaseUser", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Music");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SoundUp.Models.UserPlaylists", b =>
                {
                    b.HasOne("SoundUp.Models.Playlist", null)
                        .WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoundUp.Models.BaseUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SoundUp.Models.Album", b =>
                {
                    b.Navigation("AlbumMusic");
                });

            modelBuilder.Entity("SoundUp.Models.BaseUser", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("ListenHistory");

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("SoundUp.Models.Music", b =>
                {
                    b.Navigation("MusicAudio");

                    b.Navigation("MusicInPlaylists");

                    b.Navigation("WhoFavorited");
                });

            modelBuilder.Entity("SoundUp.Models.Playlist", b =>
                {
                    b.Navigation("MusicList");
                });

            modelBuilder.Entity("SoundUp.Models.UserAuthor", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("CreatedMusics");
                });
#pragma warning restore 612, 618
        }
    }
}
