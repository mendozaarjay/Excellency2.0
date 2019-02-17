using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class addedsecondapprovaltagging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWithSecondApproval",
                table: "ApprovalLevelAssignment",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWithSecondApproval",
                table: "ApprovalLevelAssignment");
        }
    }
}
