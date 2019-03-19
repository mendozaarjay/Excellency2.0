using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class addeduserlevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserLevelId",
                table: "Positions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_UserLevelId",
                table: "Positions",
                column: "UserLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_UserLevels_UserLevelId",
                table: "Positions",
                column: "UserLevelId",
                principalTable: "UserLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_UserLevels_UserLevelId",
                table: "Positions");

            migrationBuilder.DropTable(
                name: "UserLevels");

            migrationBuilder.DropIndex(
                name: "IX_Positions_UserLevelId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "UserLevelId",
                table: "Positions");
        }
    }
}
