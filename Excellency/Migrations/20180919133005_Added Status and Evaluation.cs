using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class AddedStatusandEvaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RateeId = table.Column<int>(nullable: true),
                    KeyResultAreaId = table.Column<int>(nullable: true),
                    RaterId = table.Column<int>(nullable: true),
                    PostedDate = table.Column<DateTime>(nullable: false),
                    ApproverId = table.Column<int>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    ApproverRemarks = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationHeader_Accounts_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationHeader_KeyResultAreas_KeyResultAreaId",
                        column: x => x.KeyResultAreaId,
                        principalTable: "KeyResultAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationHeader_Employees_RateeId",
                        column: x => x.RateeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationHeader_Accounts_RaterId",
                        column: x => x.RaterId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationHeader_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationHeaderId = table.Column<int>(nullable: true),
                    SuccessIndicatorId = table.Column<int>(nullable: true),
                    RatingTableItemId = table.Column<int>(nullable: true),
                    Score = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationLine_EvaluationHeader_EvaluationHeaderId",
                        column: x => x.EvaluationHeaderId,
                        principalTable: "EvaluationHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationLine_RatingTableItems_RatingTableItemId",
                        column: x => x.RatingTableItemId,
                        principalTable: "RatingTableItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationLine_KeySuccessIndicators_SuccessIndicatorId",
                        column: x => x.SuccessIndicatorId,
                        principalTable: "KeySuccessIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationHeader_ApproverId",
                table: "EvaluationHeader",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationHeader_KeyResultAreaId",
                table: "EvaluationHeader",
                column: "KeyResultAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationHeader_RateeId",
                table: "EvaluationHeader",
                column: "RateeId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationHeader_RaterId",
                table: "EvaluationHeader",
                column: "RaterId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationHeader_StatusId",
                table: "EvaluationHeader",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationLine_EvaluationHeaderId",
                table: "EvaluationLine",
                column: "EvaluationHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationLine_RatingTableItemId",
                table: "EvaluationLine",
                column: "RatingTableItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationLine_SuccessIndicatorId",
                table: "EvaluationLine",
                column: "SuccessIndicatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationLine");

            migrationBuilder.DropTable(
                name: "EvaluationHeader");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
