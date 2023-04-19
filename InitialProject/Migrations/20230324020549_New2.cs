using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class New2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccomodationAccId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccomodationReservationId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "TourImages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "AccomodationReservationId",
                table: "GuestRatings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccomodationReservationId",
                table: "Accomodations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccomodationReservations",
                columns: table => new
                {
                    AccomodationReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CheckInDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    AccomodationAccId = table.Column<int>(type: "INTEGER", nullable: true),
                    GuestId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccomodationReservations", x => x.AccomodationReservationId);
                    table.ForeignKey(
                        name: "FK_AccomodationReservations_Accomodations_AccomodationAccId",
                        column: x => x.AccomodationAccId,
                        principalTable: "Accomodations",
                        principalColumn: "AccId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccomodationReservations_Users_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccomodationAccId",
                table: "Users",
                column: "AccomodationAccId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccomodationReservationId",
                table: "Users",
                column: "AccomodationReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestRatings_AccomodationReservationId",
                table: "GuestRatings",
                column: "AccomodationReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Accomodations_AccomodationReservationId",
                table: "Accomodations",
                column: "AccomodationReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccomodationReservations_AccomodationAccId",
                table: "AccomodationReservations",
                column: "AccomodationAccId");

            migrationBuilder.CreateIndex(
                name: "IX_AccomodationReservations_GuestId",
                table: "AccomodationReservations",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accomodations_AccomodationReservations_AccomodationReservationId",
                table: "Accomodations",
                column: "AccomodationReservationId",
                principalTable: "AccomodationReservations",
                principalColumn: "AccomodationReservationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestRatings_AccomodationReservations_AccomodationReservationId",
                table: "GuestRatings",
                column: "AccomodationReservationId",
                principalTable: "AccomodationReservations",
                principalColumn: "AccomodationReservationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AccomodationReservations_AccomodationReservationId",
                table: "Users",
                column: "AccomodationReservationId",
                principalTable: "AccomodationReservations",
                principalColumn: "AccomodationReservationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Accomodations_AccomodationAccId",
                table: "Users",
                column: "AccomodationAccId",
                principalTable: "Accomodations",
                principalColumn: "AccId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accomodations_AccomodationReservations_AccomodationReservationId",
                table: "Accomodations");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestRatings_AccomodationReservations_AccomodationReservationId",
                table: "GuestRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AccomodationReservations_AccomodationReservationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Accomodations_AccomodationAccId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AccomodationReservations");

            migrationBuilder.DropIndex(
                name: "IX_Users_AccomodationAccId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AccomodationReservationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_GuestRatings_AccomodationReservationId",
                table: "GuestRatings");

            migrationBuilder.DropIndex(
                name: "IX_Accomodations_AccomodationReservationId",
                table: "Accomodations");

            migrationBuilder.DropColumn(
                name: "AccomodationAccId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccomodationReservationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccomodationReservationId",
                table: "GuestRatings");

            migrationBuilder.DropColumn(
                name: "AccomodationReservationId",
                table: "Accomodations");

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "TourImages",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
