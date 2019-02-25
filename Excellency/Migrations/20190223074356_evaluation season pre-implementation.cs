using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class evaluationseasonpreimplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvaluationSeasonId",
                table: "Recommendations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluationSeasonId",
                table: "RatingHeader",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluationSeasonId",
                table: "PeerEvaluationHeader",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluationSeasonId",
                table: "EmployeeKRAAssignments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluationSeasonId",
                table: "EmployeeBehavioralAssignments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_EvaluationSeasonId",
                table: "Recommendations",
                column: "EvaluationSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingHeader_EvaluationSeasonId",
                table: "RatingHeader",
                column: "EvaluationSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationHeader_EvaluationSeasonId",
                table: "PeerEvaluationHeader",
                column: "EvaluationSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeKRAAssignments_EvaluationSeasonId",
                table: "EmployeeKRAAssignments",
                column: "EvaluationSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBehavioralAssignments_EvaluationSeasonId",
                table: "EmployeeBehavioralAssignments",
                column: "EvaluationSeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBehavioralAssignments_EvaluationSeasons_EvaluationSeasonId",
                table: "EmployeeBehavioralAssignments",
                column: "EvaluationSeasonId",
                principalTable: "EvaluationSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeKRAAssignments_EvaluationSeasons_EvaluationSeasonId",
                table: "EmployeeKRAAssignments",
                column: "EvaluationSeasonId",
                principalTable: "EvaluationSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PeerEvaluationHeader_EvaluationSeasons_EvaluationSeasonId",
                table: "PeerEvaluationHeader",
                column: "EvaluationSeasonId",
                principalTable: "EvaluationSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingHeader_EvaluationSeasons_EvaluationSeasonId",
                table: "RatingHeader",
                column: "EvaluationSeasonId",
                principalTable: "EvaluationSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_EvaluationSeasons_EvaluationSeasonId",
                table: "Recommendations",
                column: "EvaluationSeasonId",
                principalTable: "EvaluationSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBehavioralAssignments_EvaluationSeasons_EvaluationSeasonId",
                table: "EmployeeBehavioralAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeKRAAssignments_EvaluationSeasons_EvaluationSeasonId",
                table: "EmployeeKRAAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_PeerEvaluationHeader_EvaluationSeasons_EvaluationSeasonId",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingHeader_EvaluationSeasons_EvaluationSeasonId",
                table: "RatingHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_EvaluationSeasons_EvaluationSeasonId",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_EvaluationSeasonId",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_RatingHeader_EvaluationSeasonId",
                table: "RatingHeader");

            migrationBuilder.DropIndex(
                name: "IX_PeerEvaluationHeader_EvaluationSeasonId",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeKRAAssignments_EvaluationSeasonId",
                table: "EmployeeKRAAssignments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeBehavioralAssignments_EvaluationSeasonId",
                table: "EmployeeBehavioralAssignments");

            migrationBuilder.DropColumn(
                name: "EvaluationSeasonId",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "EvaluationSeasonId",
                table: "RatingHeader");

            migrationBuilder.DropColumn(
                name: "EvaluationSeasonId",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropColumn(
                name: "EvaluationSeasonId",
                table: "EmployeeKRAAssignments");

            migrationBuilder.DropColumn(
                name: "EvaluationSeasonId",
                table: "EmployeeBehavioralAssignments");
        }
    }
}
