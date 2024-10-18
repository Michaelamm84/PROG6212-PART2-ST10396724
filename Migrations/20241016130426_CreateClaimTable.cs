using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6212_PART2_ST10396724.Migrations
{
    /// <inheritdoc />
    public partial class CreateClaimTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "claim",
                columns: table => new
                {
                    ClaimID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hoursWorked = table.Column<int>(type: "int", nullable: false),
                    hourlyPay = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LecturerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claim", x => x.ClaimID);
                    table.ForeignKey(
                        name: "FK_claim_lecturer_LecturerID",
                        column: x => x.LecturerID,
                        principalTable: "lecturer",
                        principalColumn: "LecturerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_claim_LecturerID",
                table: "claim",
                column: "LecturerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claim");
        }
    }
}
