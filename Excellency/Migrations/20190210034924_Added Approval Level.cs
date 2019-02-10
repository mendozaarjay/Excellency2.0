using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class AddedApprovalLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingHeader_Accounts_ApproverId",
                table: "RatingHeader");

            migrationBuilder.RenameColumn(
                name: "ApproverRemarks",
                table: "RatingHeader",
                newName: "SecondApproverRemarks");

            migrationBuilder.RenameColumn(
                name: "ApproverId",
                table: "RatingHeader",
                newName: "SecondApproverId");

            migrationBuilder.RenameColumn(
                name: "ApprovedDate",
                table: "RatingHeader",
                newName: "SecondApprovalDate");

            migrationBuilder.RenameIndex(
                name: "IX_RatingHeader_ApproverId",
                table: "RatingHeader",
                newName: "IX_RatingHeader_SecondApproverId");

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstApprovalDate",
                table: "RatingHeader",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FirstApproverId",
                table: "RatingHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstApproverRemarks",
                table: "RatingHeader",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApprovalLevelAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    FirstApprovalId = table.Column<int>(nullable: true),
                    SecondApprovalId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalLevelAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalLevelAssignment_Accounts_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalLevelAssignment_Accounts_FirstApprovalId",
                        column: x => x.FirstApprovalId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalLevelAssignment_Accounts_SecondApprovalId",
                        column: x => x.SecondApprovalId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingHeader_FirstApproverId",
                table: "RatingHeader",
                column: "FirstApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLevelAssignment_EmployeeId",
                table: "ApprovalLevelAssignment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLevelAssignment_FirstApprovalId",
                table: "ApprovalLevelAssignment",
                column: "FirstApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalLevelAssignment_SecondApprovalId",
                table: "ApprovalLevelAssignment",
                column: "SecondApprovalId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingHeader_Accounts_FirstApproverId",
                table: "RatingHeader",
                column: "FirstApproverId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingHeader_Accounts_SecondApproverId",
                table: "RatingHeader",
                column: "SecondApproverId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingHeader_Accounts_FirstApproverId",
                table: "RatingHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingHeader_Accounts_SecondApproverId",
                table: "RatingHeader");

            migrationBuilder.DropTable(
                name: "ApprovalLevelAssignment");

            migrationBuilder.DropIndex(
                name: "IX_RatingHeader_FirstApproverId",
                table: "RatingHeader");

            migrationBuilder.DropColumn(
                name: "FirstApprovalDate",
                table: "RatingHeader");

            migrationBuilder.DropColumn(
                name: "FirstApproverId",
                table: "RatingHeader");

            migrationBuilder.DropColumn(
                name: "FirstApproverRemarks",
                table: "RatingHeader");

            migrationBuilder.RenameColumn(
                name: "SecondApproverRemarks",
                table: "RatingHeader",
                newName: "ApproverRemarks");

            migrationBuilder.RenameColumn(
                name: "SecondApproverId",
                table: "RatingHeader",
                newName: "ApproverId");

            migrationBuilder.RenameColumn(
                name: "SecondApprovalDate",
                table: "RatingHeader",
                newName: "ApprovedDate");

            migrationBuilder.RenameIndex(
                name: "IX_RatingHeader_SecondApproverId",
                table: "RatingHeader",
                newName: "IX_RatingHeader_ApproverId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingHeader_Accounts_ApproverId",
                table: "RatingHeader",
                column: "ApproverId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
