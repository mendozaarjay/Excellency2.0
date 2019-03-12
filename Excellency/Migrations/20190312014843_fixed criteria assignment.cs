using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class fixedcriteriaassignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeCriteriaAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    CriteriaId = table.Column<int>(nullable: true),
                    PeriodId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCriteriaAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCriteriaAssignments_CriteriaHeaders_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "CriteriaHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeCriteriaAssignments_Accounts_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeCriteriaAssignments_EvaluationSeasons_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "EvaluationSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCriteriaAssignments_CriteriaId",
                table: "EmployeeCriteriaAssignments",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCriteriaAssignments_EmployeeId",
                table: "EmployeeCriteriaAssignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCriteriaAssignments_PeriodId",
                table: "EmployeeCriteriaAssignments",
                column: "PeriodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeCriteriaAssignments");
        }
    }
}
