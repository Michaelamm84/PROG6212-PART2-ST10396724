using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6212_PART2_ST10396724.Migrations
{
    /// <inheritdoc />
    public partial class CreateAcademigManagerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LecturerName",
                table: "lecturer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "academicManagers",
                columns: table => new
                {
                    AcademicManagerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicManagerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acaEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_academicManagers", x => x.AcademicManagerID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "academicManagers");

            migrationBuilder.AlterColumn<string>(
                name: "LecturerName",
                table: "lecturer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
