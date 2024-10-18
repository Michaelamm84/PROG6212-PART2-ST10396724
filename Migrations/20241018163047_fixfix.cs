using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6212_PART2_ST10396724.Migrations
{
    /// <inheritdoc />
    public partial class fixfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "approvalStatus",
                table: "claimApproval",
                newName: "newApprovalStatus");

            migrationBuilder.AddColumn<string>(
                name: "ApprovalStatus",
                table: "claim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "claim");

            migrationBuilder.RenameColumn(
                name: "newApprovalStatus",
                table: "claimApproval",
                newName: "approvalStatus");
        }
    }
}
