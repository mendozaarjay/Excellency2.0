using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class AddedSeasontoPeerEval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodId",
                table: "CriteriaEvaluationHeaders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaEvaluationHeaders_PeriodId",
                table: "CriteriaEvaluationHeaders",
                column: "PeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_CriteriaEvaluationHeaders_EvaluationSeasons_PeriodId",
                table: "CriteriaEvaluationHeaders",
                column: "PeriodId",
                principalTable: "EvaluationSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriteriaEvaluationHeaders_EvaluationSeasons_PeriodId",
                table: "CriteriaEvaluationHeaders");

            migrationBuilder.DropIndex(
                name: "IX_CriteriaEvaluationHeaders_PeriodId",
                table: "CriteriaEvaluationHeaders");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "CriteriaEvaluationHeaders");
        }
    }
}
