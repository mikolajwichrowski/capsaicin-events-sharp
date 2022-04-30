using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capsaicin_events_sharp.Migrations
{
    public partial class ReactionModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "message",
                table: "Reactions",
                type: "TEXT",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "availibilityDate",
                table: "Reactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "message",
                table: "Reactions",
                type: "TEXT",
                maxLength: 4000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "availibilityDate",
                table: "Reactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
