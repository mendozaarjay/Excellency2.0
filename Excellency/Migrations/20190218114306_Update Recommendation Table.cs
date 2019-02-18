using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class UpdateRecommendationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Accounts_EmlployeeId",
                table: "Recommendations");

            migrationBuilder.RenameColumn(
                name: "EmlployeeId",
                table: "Recommendations",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Recommendations_EmlployeeId",
                table: "Recommendations",
                newName: "IX_Recommendations_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Accounts_EmployeeId",
                table: "Recommendations",
                column: "EmployeeId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Accounts_EmployeeId",
                table: "Recommendations");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Recommendations",
                newName: "EmlployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Recommendations_EmployeeId",
                table: "Recommendations",
                newName: "IX_Recommendations_EmlployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Accounts_EmlployeeId",
                table: "Recommendations",
                column: "EmlployeeId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
