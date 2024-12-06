using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoundUp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RefreshTokenId = table.Column<Guid>(type: "uuid", nullable: false),
                    ListenHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserType = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_AllUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AllUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListenHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListenHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListenHistories_AllUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AllUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayLists_AllUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AllUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AllUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AllUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Music",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    MusicAudioId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Music", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Music_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Music_AllUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AllUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListenHistoryMusic",
                columns: table => new
                {
                    ListenHistoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicHistoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListenHistoryMusic", x => new { x.ListenHistoriesId, x.MusicHistoryId });
                    table.ForeignKey(
                        name: "FK_ListenHistoryMusic_ListenHistories_ListenHistoriesId",
                        column: x => x.ListenHistoriesId,
                        principalTable: "ListenHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListenHistoryMusic_Music_MusicHistoryId",
                        column: x => x.MusicHistoryId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicAudio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Audio = table.Column<string>(type: "text", nullable: false),
                    MusicId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicAudio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicAudio_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistMusic",
                columns: table => new
                {
                    PlaylistId = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistMusic", x => new { x.PlaylistId, x.MusicId });
                    table.ForeignKey(
                        name: "FK_PlaylistMusic_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistMusic_PlayLists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "PlayLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMusicFavorite",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMusicFavorite", x => new { x.UserId, x.MusicId });
                    table.ForeignKey(
                        name: "FK_UserMusicFavorite_AllUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AllUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMusicFavorite_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_AuthorId_Name",
                table: "Albums",
                columns: new[] { "AuthorId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllUsers_Name",
                table: "AllUsers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListenHistories_UserId",
                table: "ListenHistories",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListenHistoryMusic_MusicHistoryId",
                table: "ListenHistoryMusic",
                column: "MusicHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Music_AlbumId",
                table: "Music",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Music_AuthorId_Name",
                table: "Music",
                columns: new[] { "AuthorId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MusicAudio_MusicId",
                table: "MusicAudio",
                column: "MusicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMusic_MusicId",
                table: "PlaylistMusic",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMusic_PlaylistId_MusicId",
                table: "PlaylistMusic",
                columns: new[] { "PlaylistId", "MusicId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayLists_CreatorId",
                table: "PlayLists",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMusicFavorite_MusicId",
                table: "UserMusicFavorite",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMusicFavorite_UserId_MusicId",
                table: "UserMusicFavorite",
                columns: new[] { "UserId", "MusicId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListenHistoryMusic");

            migrationBuilder.DropTable(
                name: "MusicAudio");

            migrationBuilder.DropTable(
                name: "PlaylistMusic");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserMusicFavorite");

            migrationBuilder.DropTable(
                name: "ListenHistories");

            migrationBuilder.DropTable(
                name: "PlayLists");

            migrationBuilder.DropTable(
                name: "Music");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "AllUsers");
        }
    }
}
