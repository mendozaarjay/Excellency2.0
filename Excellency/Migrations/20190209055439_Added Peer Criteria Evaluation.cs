using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class AddedPeerCriteriaEvaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeerCriteriaLine_PeerCriteriaHeader_HeaderId",
                table: "PeerCriteriaLine");

            migrationBuilder.DropTable(
                name: "PeerCriteriaHeader");

            migrationBuilder.CreateTable(
                name: "PeerCriterias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerCriterias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeerEvaluationHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Employee = table.Column<int>(nullable: false),
                    EvaluatedBy = table.Column<string>(nullable: true),
                    DateEvaluated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerEvaluationHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeerEvaluationLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PeerEvaluationHeaderId = table.Column<int>(nullable: true),
                    PeerCriteriaId = table.Column<int>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerEvaluationLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeerEvaluationLine_PeerCriterias_PeerCriteriaId",
                        column: x => x.PeerCriteriaId,
                        principalTable: "PeerCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeerEvaluationLine_PeerEvaluationHeader_PeerEvaluationHeaderId",
                        column: x => x.PeerEvaluationHeaderId,
                        principalTable: "PeerEvaluationHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationLine_PeerCriteriaId",
                table: "PeerEvaluationLine",
                column: "PeerCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationLine_PeerEvaluationHeaderId",
                table: "PeerEvaluationLine",
                column: "PeerEvaluationHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeerCriteriaLine_PeerCriterias_HeaderId",
                table: "PeerCriteriaLine",
                column: "HeaderId",
                principalTable: "PeerCriterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeerCriteriaLine_PeerCriterias_HeaderId",
                table: "PeerCriteriaLine");

            migrationBuilder.DropTable(
                name: "PeerEvaluationLine");

            migrationBuilder.DropTable(
                name: "PeerCriterias");

            migrationBuilder.DropTable(
                name: "PeerEvaluationHeader");

            migrationBuilder.CreateTable(
                name: "PeerCriteriaHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerCriteriaHeader", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PeerCriteriaLine_PeerCriteriaHeader_HeaderId",
                table: "PeerCriteriaLine",
                column: "HeaderId",
                principalTable: "PeerCriteriaHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
