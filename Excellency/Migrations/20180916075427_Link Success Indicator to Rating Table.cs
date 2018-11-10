using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class LinkSuccessIndicatortoRatingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingTableId",
                table: "KeySuccessIndicators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeySuccessIndicators_RatingTableId",
                table: "KeySuccessIndicators",
                column: "RatingTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeySuccessIndicators_RatingTables_RatingTableId",
                table: "KeySuccessIndicators",
                column: "RatingTableId",
                principalTable: "RatingTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeySuccessIndicators_RatingTables_RatingTableId",
                table: "KeySuccessIndicators");

            migrationBuilder.DropIndex(
                name: "IX_KeySuccessIndicators_RatingTableId",
                table: "KeySuccessIndicators");

            migrationBuilder.DropColumn(
                name: "RatingTableId",
                table: "KeySuccessIndicators");
        }
    }
}
