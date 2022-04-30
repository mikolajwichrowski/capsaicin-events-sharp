using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capsaicin_events_sharp.Migrations
{
    public partial class Reactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userid = table.Column<int>(type: "INTEGER", nullable: false),
                    eventid = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.id);
                    table.ForeignKey(
                        name: "FK_Attendees_Events_eventid",
                        column: x => x.eventid,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendees_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventFiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    eventid = table.Column<int>(type: "INTEGER", nullable: false),
                    fileLocation = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_EventFiles_Events_eventid",
                        column: x => x.eventid,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userid = table.Column<int>(type: "INTEGER", nullable: false),
                    eventid = table.Column<int>(type: "INTEGER", nullable: false),
                    type = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    message = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    availibilityDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    createdAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reactions_Events_eventid",
                        column: x => x.eventid,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reactions_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_eventid",
                table: "Attendees",
                column: "eventid");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_userid",
                table: "Attendees",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_EventFiles_eventid",
                table: "EventFiles",
                column: "eventid");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_eventid",
                table: "Reactions",
                column: "eventid");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_userid",
                table: "Reactions",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "EventFiles");

            migrationBuilder.DropTable(
                name: "Reactions");
        }
    }
}
