using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Excellency.Migrations
{
    public partial class CreatedNewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    LogoUrl = table.Column<string>(maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Criterias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationSeasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSeasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    YearCovered = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsOpen = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interpretations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ScoreFrom = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ScoreTo = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interpretations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyResultAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyResultAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

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
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Score = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatingTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingTables", x => x.Id);
                });

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
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BehavioralFactors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehavioralFactors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BehavioralFactors_EmployeeCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "EmployeeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeerCriteriaLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HeaderId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerCriteriaLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeerCriteriaLine_PeerCriterias_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "PeerCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KeySuccessIndicators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    KeyResultAreaId = table.Column<int>(nullable: true),
                    RatingTableId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeySuccessIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeySuccessIndicators_KeyResultAreas_KeyResultAreaId",
                        column: x => x.KeyResultAreaId,
                        principalTable: "KeyResultAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeySuccessIndicators_RatingTables_RatingTableId",
                        column: x => x.RatingTableId,
                        principalTable: "RatingTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RatingTableItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RatingTableId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingTableItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingTableItems_RatingTables_RatingTableId",
                        column: x => x.RatingTableId,
                        principalTable: "RatingTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeNo = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    IsDeactivated = table.Column<bool>(nullable: false),
                    IsChangedPassword = table.Column<bool>(nullable: false),
                    IsExpired = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_EmployeeCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "EmployeeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BehavioralFactorItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    BehavioralFactorId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehavioralFactorItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BehavioralFactorItems_BehavioralFactors_BehavioralFactorId",
                        column: x => x.BehavioralFactorId,
                        principalTable: "BehavioralFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    SuccessIndicatorId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_KeySuccessIndicators_SuccessIndicatorId",
                        column: x => x.SuccessIndicatorId,
                        principalTable: "KeySuccessIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountRoleAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: true),
                    AccountId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoleAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountRoleAssignments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountRoleAssignments_AccountRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccountRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalLevelAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    FirstApprovalId = table.Column<int>(nullable: true),
                    SecondApprovalId = table.Column<int>(nullable: true),
                    IsWithSecondApproval = table.Column<bool>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ApproverAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: true),
                    ApproverId = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApproverAssignments_Accounts_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApproverAssignments_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAssignments_Accounts_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_EvaluationHeader_Accounts_RateeId",
                        column: x => x.RateeId,
                        principalTable: "Accounts",
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

            migrationBuilder.CreateTable(
                name: "PeerEvaluationHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    CreatedById = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsExpired = table.Column<bool>(nullable: false),
                    StatusId = table.Column<int>(nullable: true),
                    EvaluationSeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeerEvaluationHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeerEvaluationHeader_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeerEvaluationHeader_Accounts_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeerEvaluationHeader_EvaluationSeasons_EvaluationSeasonId",
                        column: x => x.EvaluationSeasonId,
                        principalTable: "EvaluationSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeerEvaluationHeader_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaterHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RaterId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaterHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaterHeader_Accounts_RaterId",
                        column: x => x.RaterId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RatingHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    RateeId = table.Column<int>(nullable: true),
                    RaterId = table.Column<int>(nullable: true),
                    PostedDate = table.Column<DateTime>(nullable: false),
                    FirstApproverId = table.Column<int>(nullable: true),
                    FirstApproverRemarks = table.Column<string>(nullable: true),
                    FirstApprovalDate = table.Column<DateTime>(nullable: false),
                    SecondApproverId = table.Column<int>(nullable: true),
                    SecondApproverRemarks = table.Column<string>(nullable: true),
                    SecondApprovalDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    EvaluationSeasonId = table.Column<int>(nullable: true),
                    IsExpired = table.Column<bool>(nullable: false),
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
                        name: "FK_RatingHeader_EvaluationSeasons_EvaluationSeasonId",
                        column: x => x.EvaluationSeasonId,
                        principalTable: "EvaluationSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingHeader_Accounts_FirstApproverId",
                        column: x => x.FirstApproverId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingHeader_Accounts_RateeId",
                        column: x => x.RateeId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingHeader_Accounts_RaterId",
                        column: x => x.RaterId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingHeader_Accounts_SecondApproverId",
                        column: x => x.SecondApproverId,
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
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    IsExpired = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EvaluationSeasonId = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendations_Accounts_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recommendations_EvaluationSeasons_EvaluationSeasonId",
                        column: x => x.EvaluationSeasonId,
                        principalTable: "EvaluationSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    AccountId = table.Column<int>(nullable: true),
                    IsUser = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registrations_Accounts_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBehavioralAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeAssignmentId = table.Column<int>(nullable: true),
                    BehavioralFactorId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EvaluationSeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBehavioralAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBehavioralAssignments_BehavioralFactors_BehavioralFactorId",
                        column: x => x.BehavioralFactorId,
                        principalTable: "BehavioralFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeBehavioralAssignments_EmployeeAssignments_EmployeeAssignmentId",
                        column: x => x.EmployeeAssignmentId,
                        principalTable: "EmployeeAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeBehavioralAssignments_EvaluationSeasons_EvaluationSeasonId",
                        column: x => x.EvaluationSeasonId,
                        principalTable: "EvaluationSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeKRAAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeAssignmentId = table.Column<int>(nullable: true),
                    KeyResultAreaId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EvaluationSeasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeKRAAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeKRAAssignments_EmployeeAssignments_EmployeeAssignmentId",
                        column: x => x.EmployeeAssignmentId,
                        principalTable: "EmployeeAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeKRAAssignments_EvaluationSeasons_EvaluationSeasonId",
                        column: x => x.EvaluationSeasonId,
                        principalTable: "EvaluationSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeKRAAssignments_KeyResultAreas_KeyResultAreaId",
                        column: x => x.KeyResultAreaId,
                        principalTable: "KeyResultAreas",
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

            migrationBuilder.CreateTable(
                name: "RaterLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeRaterId = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaterLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaterLine_Accounts_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RaterLine_RaterHeader_EmployeeRaterId",
                        column: x => x.EmployeeRaterId,
                        principalTable: "RaterHeader",
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
                name: "IX_AccountRoleAssignments_AccountId",
                table: "AccountRoleAssignments",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoleAssignments_RoleId",
                table: "AccountRoleAssignments",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BranchId",
                table: "Accounts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CategoryId",
                table: "Accounts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CompanyId",
                table: "Accounts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_DepartmentId",
                table: "Accounts",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PositionId",
                table: "Accounts",
                column: "PositionId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignments_ApproverId",
                table: "ApproverAssignments",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverAssignments_UserId",
                table: "ApproverAssignments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BehavioralFactorItems_BehavioralFactorId",
                table: "BehavioralFactorItems",
                column: "BehavioralFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_BehavioralFactors_CategoryId",
                table: "BehavioralFactors",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CompanyId",
                table: "Branches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SuccessIndicatorId",
                table: "Categories",
                column: "SuccessIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAssignments_EmployeeId",
                table: "EmployeeAssignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBehavioralAssignments_BehavioralFactorId",
                table: "EmployeeBehavioralAssignments",
                column: "BehavioralFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBehavioralAssignments_EmployeeAssignmentId",
                table: "EmployeeBehavioralAssignments",
                column: "EmployeeAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBehavioralAssignments_EvaluationSeasonId",
                table: "EmployeeBehavioralAssignments",
                column: "EvaluationSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeKRAAssignments_EmployeeAssignmentId",
                table: "EmployeeKRAAssignments",
                column: "EmployeeAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeKRAAssignments_EvaluationSeasonId",
                table: "EmployeeKRAAssignments",
                column: "EvaluationSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeKRAAssignments_KeyResultAreaId",
                table: "EmployeeKRAAssignments",
                column: "KeyResultAreaId");

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

            migrationBuilder.CreateIndex(
                name: "IX_KeySuccessIndicators_KeyResultAreaId",
                table: "KeySuccessIndicators",
                column: "KeyResultAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_KeySuccessIndicators_RatingTableId",
                table: "KeySuccessIndicators",
                column: "RatingTableId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerAssignment_RateeId",
                table: "PeerAssignment",
                column: "RateeId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerAssignment_RaterId",
                table: "PeerAssignment",
                column: "RaterId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerCriteriaLine_HeaderId",
                table: "PeerCriteriaLine",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationHeader_CreatedById",
                table: "PeerEvaluationHeader",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationHeader_EmployeeId",
                table: "PeerEvaluationHeader",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationHeader_EvaluationSeasonId",
                table: "PeerEvaluationHeader",
                column: "EvaluationSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationHeader_StatusId",
                table: "PeerEvaluationHeader",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationLine_PeerCriteriaId",
                table: "PeerEvaluationLine",
                column: "PeerCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_PeerEvaluationLine_PeerEvaluationHeaderId",
                table: "PeerEvaluationLine",
                column: "PeerEvaluationHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_RaterHeader_RaterId",
                table: "RaterHeader",
                column: "RaterId");

            migrationBuilder.CreateIndex(
                name: "IX_RaterLine_EmployeeId",
                table: "RaterLine",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RaterLine_EmployeeRaterId",
                table: "RaterLine",
                column: "EmployeeRaterId");

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
                name: "IX_RatingHeader_EvaluationSeasonId",
                table: "RatingHeader",
                column: "EvaluationSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingHeader_FirstApproverId",
                table: "RatingHeader",
                column: "FirstApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingHeader_RateeId",
                table: "RatingHeader",
                column: "RateeId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingHeader_RaterId",
                table: "RatingHeader",
                column: "RaterId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingHeader_SecondApproverId",
                table: "RatingHeader",
                column: "SecondApproverId");

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

            migrationBuilder.CreateIndex(
                name: "IX_RatingTableItems_RatingTableId",
                table: "RatingTableItems",
                column: "RatingTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_EmployeeId",
                table: "Recommendations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_EvaluationSeasonId",
                table: "Recommendations",
                column: "EvaluationSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_AccountId",
                table: "Registrations",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EmployeeId",
                table: "Registrations",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRoleAssignments");

            migrationBuilder.DropTable(
                name: "ApprovalLevelAssignment");

            migrationBuilder.DropTable(
                name: "ApproverAssignments");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Criterias");

            migrationBuilder.DropTable(
                name: "EmployeeBehavioralAssignments");

            migrationBuilder.DropTable(
                name: "EmployeeKRAAssignments");

            migrationBuilder.DropTable(
                name: "EvaluationLine");

            migrationBuilder.DropTable(
                name: "EvaluationSettings");

            migrationBuilder.DropTable(
                name: "Interpretations");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "PeerAssignment");

            migrationBuilder.DropTable(
                name: "PeerCriteriaLine");

            migrationBuilder.DropTable(
                name: "PeerEvaluationLine");

            migrationBuilder.DropTable(
                name: "RaterLine");

            migrationBuilder.DropTable(
                name: "RatingBehavioralFactors");

            migrationBuilder.DropTable(
                name: "RatingKeySuccessAreas");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropTable(
                name: "EmployeeAssignments");

            migrationBuilder.DropTable(
                name: "EvaluationHeader");

            migrationBuilder.DropTable(
                name: "RatingTableItems");

            migrationBuilder.DropTable(
                name: "PeerCriterias");

            migrationBuilder.DropTable(
                name: "PeerEvaluationHeader");

            migrationBuilder.DropTable(
                name: "RaterHeader");

            migrationBuilder.DropTable(
                name: "BehavioralFactorItems");

            migrationBuilder.DropTable(
                name: "KeySuccessIndicators");

            migrationBuilder.DropTable(
                name: "RatingHeader");

            migrationBuilder.DropTable(
                name: "BehavioralFactors");

            migrationBuilder.DropTable(
                name: "KeyResultAreas");

            migrationBuilder.DropTable(
                name: "RatingTables");

            migrationBuilder.DropTable(
                name: "EvaluationSeasons");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "EmployeeCategories");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
