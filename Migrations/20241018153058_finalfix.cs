using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6212_PART2_ST10396724.Migrations
{
    /// <inheritdoc />
    public partial class finalfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "claimApproval",
                columns: table => new
                {
                    ClaimApprovalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    approvalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgCoOrdinatorID = table.Column<int>(type: "int", nullable: false),
                    ClaimID = table.Column<int>(type: "int", nullable: false),
                    AcademicManagerID = table.Column<int>(type: "int", nullable: false),
                    LecturerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claimApproval", x => x.ClaimApprovalID);
                    table.ForeignKey(
                        name: "FK_claimApproval_academicManagers_AcademicManagerID",
                        column: x => x.AcademicManagerID,
                        principalTable: "academicManagers",
                        principalColumn: "AcademicManagerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_claimApproval_claim_ClaimID",
                        column: x => x.ClaimID,
                        principalTable: "claim",
                        principalColumn: "ClaimID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_claimApproval_lecturer_LecturerID",
                        column: x => x.LecturerID,
                        principalTable: "lecturer",
                        principalColumn: "LecturerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_claimApproval_progs_ProgCoOrdinatorID",
                        column: x => x.ProgCoOrdinatorID,
                        principalTable: "progs",
                        principalColumn: "ProgCoOrdinatorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_claimApproval_AcademicManagerID",
                table: "claimApproval",
                column: "AcademicManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_claimApproval_ClaimID",
                table: "claimApproval",
                column: "ClaimID");

            migrationBuilder.CreateIndex(
                name: "IX_claimApproval_LecturerID",
                table: "claimApproval",
                column: "LecturerID");

            migrationBuilder.CreateIndex(
                name: "IX_claimApproval_ProgCoOrdinatorID",
                table: "claimApproval",
                column: "ProgCoOrdinatorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claimApproval");
        }
    }
}
