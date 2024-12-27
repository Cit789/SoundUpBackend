using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoundUp.Migrations
{
    /// <inheritdoc />
    public partial class UserAndFavoriteAlbums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlbumBaseUser",
                columns: table => new
                {
                    FavoriteAlbumsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FavoritedUsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumBaseUser", x => new { x.FavoriteAlbumsId, x.FavoritedUsersId });
                    table.ForeignKey(
                        name: "FK_AlbumBaseUser_Albums_FavoriteAlbumsId",
                        column: x => x.FavoriteAlbumsId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumBaseUser_AllUsers_FavoritedUsersId",
                        column: x => x.FavoritedUsersId,
                        principalTable: "AllUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumBaseUser_FavoritedUsersId",
                table: "AlbumBaseUser",
                column: "FavoritedUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumBaseUser");
        }
    }
}
