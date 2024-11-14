using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoundUp.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "Music",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Audio = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
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
                name: "BaseUserMusic",
                columns: table => new
                {
                    FavoritesId = table.Column<Guid>(type: "uuid", nullable: false),
                    WhoFavoritedId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseUserMusic", x => new { x.FavoritesId, x.WhoFavoritedId });
                    table.ForeignKey(
                        name: "FK_BaseUserMusic_AllUsers_WhoFavoritedId",
                        column: x => x.WhoFavoritedId,
                        principalTable: "AllUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseUserMusic_Music_FavoritesId",
                        column: x => x.FavoritesId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicPlaylist",
                columns: table => new
                {
                    MusicInPlaylistsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPlaylist", x => new { x.MusicInPlaylistsId, x.MusicListId });
                    table.ForeignKey(
                        name: "FK_MusicPlaylist_Music_MusicListId",
                        column: x => x.MusicListId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicPlaylist_PlayLists_MusicInPlaylistsId",
                        column: x => x.MusicInPlaylistsId,
                        principalTable: "PlayLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_AuthorId",
                table: "Albums",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseUserMusic_WhoFavoritedId",
                table: "BaseUserMusic",
                column: "WhoFavoritedId");

            migrationBuilder.CreateIndex(
                name: "IX_Music_AlbumId",
                table: "Music",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Music_AuthorId",
                table: "Music",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlaylist_MusicListId",
                table: "MusicPlaylist",
                column: "MusicListId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayLists_CreatorId",
                table: "PlayLists",
                column: "CreatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseUserMusic");

            migrationBuilder.DropTable(
                name: "MusicPlaylist");

            migrationBuilder.DropTable(
                name: "Music");

            migrationBuilder.DropTable(
                name: "PlayLists");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "AllUsers");
        }
    }
}
