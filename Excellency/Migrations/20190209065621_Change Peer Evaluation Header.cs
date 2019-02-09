using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class ChangePeerEvaluationHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvaluatedBy",
                table: "PeerEvaluationHeader");

            migrationBuilder.RenameColumn(
                name: "DateEvaluated",
                table: "PeerEvaluationHeader",
                newName: "DateCreated");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PeerEvaluationHeader",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PeerEvaluationHeader");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "PeerEvaluationHeader",
                newName: "DateEvaluated");

            migrationBuilder.AddColumn<string>(
                name: "EvaluatedBy",
                table: "PeerEvaluationHeader",
                nullable: true);
        }
    }
}
