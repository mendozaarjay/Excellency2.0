using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class EmployeeAssignmentUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeAssignmentId",
                table: "EmployeeKRAAssignments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeAssignmentId",
                table: "EmployeeBehavioralAssignments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeKRAAssignments_EmployeeAssignmentId",
                table: "EmployeeKRAAssignments",
                column: "EmployeeAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBehavioralAssignments_EmployeeAssignmentId",
                table: "EmployeeBehavioralAssignments",
                column: "EmployeeAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBehavioralAssignments_EmployeeAssignments_EmployeeAssignmentId",
                table: "EmployeeBehavioralAssignments",
                column: "EmployeeAssignmentId",
                principalTable: "EmployeeAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeKRAAssignments_EmployeeAssignments_EmployeeAssignmentId",
                table: "EmployeeKRAAssignments",
                column: "EmployeeAssignmentId",
                principalTable: "EmployeeAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBehavioralAssignments_EmployeeAssignments_EmployeeAssignmentId",
                table: "EmployeeBehavioralAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeKRAAssignments_EmployeeAssignments_EmployeeAssignmentId",
                table: "EmployeeKRAAssignments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeKRAAssignments_EmployeeAssignmentId",
                table: "EmployeeKRAAssignments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeBehavioralAssignments_EmployeeAssignmentId",
                table: "EmployeeBehavioralAssignments");

            migrationBuilder.DropColumn(
                name: "EmployeeAssignmentId",
                table: "EmployeeKRAAssignments");

            migrationBuilder.DropColumn(
                name: "EmployeeAssignmentId",
                table: "EmployeeBehavioralAssignments");
        }
    }
}
