using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDFilm.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRefreshTokenRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                schema: "ddfilm",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_UserId",
                schema: "ddfilm",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "ddfilm",
                table: "RefreshToken");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "ddfilm",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_ApplicationUserId",
                schema: "ddfilm",
                table: "RefreshToken",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_ApplicationUserId",
                schema: "ddfilm",
                table: "RefreshToken",
                column: "ApplicationUserId",
                principalSchema: "ddfilm",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_ApplicationUserId",
                schema: "ddfilm",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_ApplicationUserId",
                schema: "ddfilm",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "ddfilm",
                table: "RefreshToken");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "ddfilm",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "ddfilm",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                schema: "ddfilm",
                table: "RefreshToken",
                column: "UserId",
                principalSchema: "ddfilm",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
