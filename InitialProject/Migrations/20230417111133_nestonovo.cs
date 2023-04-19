using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class nestonovo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.CreateTable(
                name: "OwnerReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cleanliness = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerFairness = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    AccomodationReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerReviews_AccomodationReservations_AccomodationReservationId",
                        column: x => x.AccomodationReservationId,
                        principalTable: "AccomodationReservations",
                        principalColumn: "AccomodationReservationId",
                        onDelete: ReferentialAction.Cascade);
                }); */

            migrationBuilder.CreateTable(
                name: "ReservationReschedulingRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    ReservationAccomodationReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewStartingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NewEndingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Achievable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationReschedulingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationReschedulingRequests_AccomodationReservations_ReservationAccomodationReservationId",
                        column: x => x.ReservationAccomodationReservationId,
                        principalTable: "AccomodationReservations",
                        principalColumn: "AccomodationReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            /*migrationBuilder.CreateIndex(
                name: "IX_OwnerReviews_AccomodationReservationId",
                table: "OwnerReviews",
                column: "AccomodationReservationId");*/

            migrationBuilder.CreateIndex(
                name: "IX_ReservationReschedulingRequests_ReservationAccomodationReservationId",
                table: "ReservationReschedulingRequests",
                column: "ReservationAccomodationReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerReviews");

            migrationBuilder.DropTable(
                name: "ReservationReschedulingRequests");
        }
    }
}
