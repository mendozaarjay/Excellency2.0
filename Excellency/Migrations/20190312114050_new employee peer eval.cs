using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class newemployeepeereval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CriteriaEvaluationHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RaterId = table.Column<int>(nullable: true),
                    RateeId = table.Column<int>(nullable: true),
                    CriteriaId = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaEvaluationHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriteriaEvaluationHeaders_CriteriaHeaders_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "CriteriaHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CriteriaEvaluationHeaders_Accounts_RateeId",
                        column: x => x.RateeId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CriteriaEvaluationHeaders_Accounts_RaterId",
                        column: x => x.RaterId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CriteriaEvaluationHeaders_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CriteriaEvaluationLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HeaderId = table.Column<int>(nullable: true),
                    CriteriaLineId = table.Column<int>(nullable: true),
                    Score = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaEvaluationLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriteriaEvaluationLines_CriteriaLines_CriteriaLineId",
                        column: x => x.CriteriaLineId,
                        principalTable: "CriteriaLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CriteriaEvaluationLines_CriteriaEvaluationHeaders_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "CriteriaEvaluationHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaEvaluationHeaders_CriteriaId",
                table: "CriteriaEvaluationHeaders",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaEvaluationHeaders_RateeId",
                table: "CriteriaEvaluationHeaders",
                column: "RateeId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaEvaluationHeaders_RaterId",
                table: "CriteriaEvaluationHeaders",
                column: "RaterId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaEvaluationHeaders_StatusId",
                table: "CriteriaEvaluationHeaders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaEvaluationLines_CriteriaLineId",
                table: "CriteriaEvaluationLines",
                column: "CriteriaLineId");

            migrationBuilder.CreateIndex(
                name: "IX_CriteriaEvaluationLines_HeaderId",
                table: "CriteriaEvaluationLines",
                column: "HeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriteriaEvaluationLines");

            migrationBuilder.DropTable(
                name: "CriteriaEvaluationHeaders");
        }
    }
}
