using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class Igor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccomodationImages_accomodationRatings_AccomodationRatingAccomodationId",
                table: "AccomodationImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_accomodationRatings_AccomodationRatingAccomodationId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accomodationRatings",
                table: "accomodationRatings");

            migrationBuilder.RenameTable(
                name: "accomodationRatings",
                newName: "AccomodationRating");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccomodationRating",
                table: "AccomodationRating",
                column: "AccomodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccomodationImages_AccomodationRating_AccomodationRatingAccomodationId",
                table: "AccomodationImages",
                column: "AccomodationRatingAccomodationId",
                principalTable: "AccomodationRating",
                principalColumn: "AccomodationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AccomodationRating_AccomodationRatingAccomodationId",
                table: "Comments",
                column: "AccomodationRatingAccomodationId",
                principalTable: "AccomodationRating",
                principalColumn: "AccomodationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccomodationImages_AccomodationRating_AccomodationRatingAccomodationId",
                table: "AccomodationImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AccomodationRating_AccomodationRatingAccomodationId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccomodationRating",
                table: "AccomodationRating");

            migrationBuilder.RenameTable(
                name: "AccomodationRating",
                newName: "accomodationRatings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accomodationRatings",
                table: "accomodationRatings",
                column: "AccomodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccomodationImages_accomodationRatings_AccomodationRatingAccomodationId",
                table: "AccomodationImages",
                column: "AccomodationRatingAccomodationId",
                principalTable: "accomodationRatings",
                principalColumn: "AccomodationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_accomodationRatings_AccomodationRatingAccomodationId",
                table: "Comments",
                column: "AccomodationRatingAccomodationId",
                principalTable: "accomodationRatings",
                principalColumn: "AccomodationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
