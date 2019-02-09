using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class Addedauditinpeerevaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropColumn(
                name: "Employee",
                table: "PeerEvaluationHeader");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "PeerEvaluationHeader",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "PeerEvaluationHeader",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationHeader_CreatedById",
                table: "PeerEvaluationHeader",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationHeader_EmployeeId",
                table: "PeerEvaluationHeader",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeerEvaluationHeader_Accounts_CreatedById",
                table: "PeerEvaluationHeader",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PeerEvaluationHeader_Accounts_EmployeeId",
                table: "PeerEvaluationHeader",
                column: "EmployeeId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeerEvaluationHeader_Accounts_CreatedById",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_PeerEvaluationHeader_Accounts_EmployeeId",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropIndex(
                name: "IX_PeerEvaluationHeader_CreatedById",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropIndex(
                name: "IX_PeerEvaluationHeader_EmployeeId",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "PeerEvaluationHeader");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PeerEvaluationHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Employee",
                table: "PeerEvaluationHeader",
                nullable: false,
                defaultValue: 0);
        }
    }
}
