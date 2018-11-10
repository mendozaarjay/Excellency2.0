using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class AddedNewRatingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RatingHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RateeId = table.Column<int>(nullable: true),
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
                    table.PrimaryKey("PK_RatingHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingHeader_Accounts_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingHeader_Employees_RateeId",
                        column: x => x.RateeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingHeader_Accounts_RaterId",
                        column: x => x.RaterId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingHeader_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RatingBehavioralFactors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RatingHeaderId = table.Column<int>(nullable: true),
                    BehavioralFactorId = table.Column<int>(nullable: true),
                    BehavioralFactorItemId = table.Column<int>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingBehavioralFactors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingBehavioralFactors_BehavioralFactors_BehavioralFactorId",
                        column: x => x.BehavioralFactorId,
                        principalTable: "BehavioralFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingBehavioralFactors_BehavioralFactorItems_BehavioralFactorItemId",
                        column: x => x.BehavioralFactorItemId,
                        principalTable: "BehavioralFactorItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingBehavioralFactors_RatingHeader_RatingHeaderId",
                        column: x => x.RatingHeaderId,
                        principalTable: "RatingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RatingKeySuccessAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RatingHeaderId = table.Column<int>(nullable: true),
                    KeyResultAreaId = table.Column<int>(nullable: true),
                    KeySuccessIndicatorId = table.Column<int>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingKeySuccessAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingKeySuccessAreas_KeyResultAreas_KeyResultAreaId",
                        column: x => x.KeyResultAreaId,
                        principalTable: "KeyResultAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingKeySuccessAreas_KeySuccessIndicators_KeySuccessIndicatorId",
                        column: x => x.KeySuccessIndicatorId,
                        principalTable: "KeySuccessIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingKeySuccessAreas_RatingHeader_RatingHeaderId",
                        column: x => x.RatingHeaderId,
                        principalTable: "RatingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingBehavioralFactors_BehavioralFactorId",
                table: "RatingBehavioralFactors",
                column: "BehavioralFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBehavioralFactors_BehavioralFactorItemId",
                table: "RatingBehavioralFactors",
                column: "BehavioralFactorItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBehavioralFactors_RatingHeaderId",
                table: "RatingBehavioralFactors",
                column: "RatingHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingHeader_ApproverId",
                table: "RatingHeader",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingHeader_RateeId",
                table: "RatingHeader",
                column: "RateeId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingHeader_RaterId",
                table: "RatingHeader",
                column: "RaterId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingHeader_StatusId",
                table: "RatingHeader",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingKeySuccessAreas_KeyResultAreaId",
                table: "RatingKeySuccessAreas",
                column: "KeyResultAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingKeySuccessAreas_KeySuccessIndicatorId",
                table: "RatingKeySuccessAreas",
                column: "KeySuccessIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingKeySuccessAreas_RatingHeaderId",
                table: "RatingKeySuccessAreas",
                column: "RatingHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingBehavioralFactors");

            migrationBuilder.DropTable(
                name: "RatingKeySuccessAreas");

            migrationBuilder.DropTable(
                name: "RatingHeader");
        }
    }
}
