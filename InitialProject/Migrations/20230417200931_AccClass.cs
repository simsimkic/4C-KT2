using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class AccClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GuestRatings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Accomodations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "AccomodationReservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_GuestRatings_UserId",
                table: "GuestRatings",
                column: "UserId");

            /*migrationBuilder.AddForeignKey(
                name: "FK_GuestRatings_Users_UserId",
                table: "GuestRatings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId"); */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestRatings_Users_UserId",
                table: "GuestRatings");

            migrationBuilder.DropIndex(
                name: "IX_GuestRatings_UserId",
                table: "GuestRatings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GuestRatings");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Accomodations");

            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "AccomodationReservations");
        }
    }
}
