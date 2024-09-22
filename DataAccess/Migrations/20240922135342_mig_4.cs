using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactForms_Users_UserId",
                table: "ContactForms");

            migrationBuilder.DropIndex(
                name: "IX_ContactForms_UserId",
                table: "ContactForms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ContactForms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ContactForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContactForms_UserId",
                table: "ContactForms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactForms_Users_UserId",
                table: "ContactForms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
