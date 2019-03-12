using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class addedevaluationconfirmation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "RatingHeader",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "RatingHeader");
        }
    }
}
