using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capsaicin_events_sharp.Migrations
{
    public partial class UserConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_username",
                table: "Users",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_username",
                table: "Users");
        }
    }
}
