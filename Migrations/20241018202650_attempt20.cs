using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6212_PART2_ST10396724.Migrations
{
    /// <inheritdoc />
    public partial class attempt20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "document");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileBinary",
                table: "document",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileBinary",
                table: "document");

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "document",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
