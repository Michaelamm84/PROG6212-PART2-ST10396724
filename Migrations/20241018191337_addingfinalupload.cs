using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6212_PART2_ST10396724.Migrations
{
    /// <inheritdoc />
    public partial class addingfinalupload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_claim_ClaimID",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_lecturer_LecturerID",
                table: "Document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "document");

            migrationBuilder.RenameIndex(
                name: "IX_Document_LecturerID",
                table: "document",
                newName: "IX_document_LecturerID");

            migrationBuilder.RenameIndex(
                name: "IX_Document_ClaimID",
                table: "document",
                newName: "IX_document_ClaimID");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "document",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_document",
                table: "document",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_document_claim_ClaimID",
                table: "document",
                column: "ClaimID",
                principalTable: "claim",
                principalColumn: "ClaimID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_document_lecturer_LecturerID",
                table: "document",
                column: "LecturerID",
                principalTable: "lecturer",
                principalColumn: "LecturerID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_claim_ClaimID",
                table: "document");

            migrationBuilder.DropForeignKey(
                name: "FK_document_lecturer_LecturerID",
                table: "document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_document",
                table: "document");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "document");

            migrationBuilder.RenameTable(
                name: "document",
                newName: "Document");

            migrationBuilder.RenameIndex(
                name: "IX_document_LecturerID",
                table: "Document",
                newName: "IX_Document_LecturerID");

            migrationBuilder.RenameIndex(
                name: "IX_document_ClaimID",
                table: "Document",
                newName: "IX_Document_ClaimID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_claim_ClaimID",
                table: "Document",
                column: "ClaimID",
                principalTable: "claim",
                principalColumn: "ClaimID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_lecturer_LecturerID",
                table: "Document",
                column: "LecturerID",
                principalTable: "lecturer",
                principalColumn: "LecturerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
