using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class addedpeerassignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeerAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RateeId = table.Column<int>(nullable: true),
                    RaterId = table.Column<int>(nullable: true),
                    IsExpired = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeerAssignment_Accounts_RateeId",
                        column: x => x.RateeId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeerAssignment_Accounts_RaterId",
                        column: x => x.RaterId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeerAssignment_RateeId",
                table: "PeerAssignment",
                column: "RateeId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerAssignment_RaterId",
                table: "PeerAssignment",
                column: "RaterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeerAssignment");
        }
    }
}
