using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class TourResNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristNotifications_Tours_TourId",
                table: "TouristNotifications");

            migrationBuilder.RenameColumn(
                name: "TourId",
                table: "TouristNotifications",
                newName: "TourReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_TouristNotifications_TourId",
                table: "TouristNotifications",
                newName: "IX_TouristNotifications_TourReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristNotifications_TourReservations_TourReservationId",
                table: "TouristNotifications",
                column: "TourReservationId",
                principalTable: "TourReservations",
                principalColumn: "TourReservationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristNotifications_TourReservations_TourReservationId",
                table: "TouristNotifications");

            migrationBuilder.RenameColumn(
                name: "TourReservationId",
                table: "TouristNotifications",
                newName: "TourId");

            migrationBuilder.RenameIndex(
                name: "IX_TouristNotifications_TourReservationId",
                table: "TouristNotifications",
                newName: "IX_TouristNotifications_TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristNotifications_Tours_TourId",
                table: "TouristNotifications",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "TourId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
