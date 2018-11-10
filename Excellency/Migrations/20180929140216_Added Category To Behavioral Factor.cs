using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class AddedCategoryToBehavioralFactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BehavioralFactors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BehavioralFactors_CategoryId",
                table: "BehavioralFactors",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BehavioralFactors_EmployeeCategories_CategoryId",
                table: "BehavioralFactors",
                column: "CategoryId",
                principalTable: "EmployeeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehavioralFactors_EmployeeCategories_CategoryId",
                table: "BehavioralFactors");

            migrationBuilder.DropIndex(
                name: "IX_BehavioralFactors_CategoryId",
                table: "BehavioralFactors");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BehavioralFactors");
        }
    }
}
