using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class AddedStatusinPeerEvaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PeerEvaluationHeader",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationHeader_StatusId",
                table: "PeerEvaluationHeader",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeerEvaluationHeader_Statuses_StatusId",
                table: "PeerEvaluationHeader",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeerEvaluationHeader_Statuses_StatusId",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropIndex(
                name: "IX_PeerEvaluationHeader_StatusId",
                table: "PeerEvaluationHeader");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PeerEvaluationHeader");
        }
    }
}
