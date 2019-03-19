using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class changeuserleveltopositionlevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_UserLevels_UserLevelId",
                table: "Positions");

            migrationBuilder.DropTable(
                name: "UserLevels");

            migrationBuilder.RenameColumn(
                name: "UserLevelId",
                table: "Positions",
                newName: "PositionLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_UserLevelId",
                table: "Positions",
                newName: "IX_Positions_PositionLevelId");

            migrationBuilder.CreateTable(
                name: "PositionLevels",
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
                    table.PrimaryKey("PK_PositionLevels", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_PositionLevels_PositionLevelId",
                table: "Positions",
                column: "PositionLevelId",
                principalTable: "PositionLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_PositionLevels_PositionLevelId",
                table: "Positions");

            migrationBuilder.DropTable(
                name: "PositionLevels");

            migrationBuilder.RenameColumn(
                name: "PositionLevelId",
                table: "Positions",
                newName: "UserLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_PositionLevelId",
                table: "Positions",
                newName: "IX_Positions_UserLevelId");

            migrationBuilder.CreateTable(
                name: "UserLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLevels", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_UserLevels_UserLevelId",
                table: "Positions",
                column: "UserLevelId",
                principalTable: "UserLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
