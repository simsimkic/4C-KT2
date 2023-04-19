using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    public partial class tourrat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TourRatingId",
                table: "TourImages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TourRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuideKnowledge = table.Column<int>(type: "INTEGER", nullable: false),
                    GuideLanguage = table.Column<int>(type: "INTEGER", nullable: false),
                    TourAmusement = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    TourId = table.Column<int>(type: "INTEGER", nullable: true),
                    TouristId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourRatings_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourRatings_Users_TouristId",
                        column: x => x.TouristId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourImages_TourRatingId",
                table: "TourImages",
                column: "TourRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_TourRatings_TourId",
                table: "TourRatings",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourRatings_TouristId",
                table: "TourRatings",
                column: "TouristId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourImages_TourRatings_TourRatingId",
                table: "TourImages",
                column: "TourRatingId",
                principalTable: "TourRatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourImages_TourRatings_TourRatingId",
                table: "TourImages");

            migrationBuilder.DropTable(
                name: "TourRatings");

            migrationBuilder.DropIndex(
                name: "IX_TourImages_TourRatingId",
                table: "TourImages");

            migrationBuilder.DropColumn(
                name: "TourRatingId",
                table: "TourImages");
        }
    }
}
