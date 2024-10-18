﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6212_PART2_ST10396724.Migrations
{
    /// <inheritdoc />
    public partial class CreateProgCoTableFINAL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_claim_lecturer_LecturerID",
                table: "claim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_claim",
                table: "claim");

            migrationBuilder.RenameTable(
                name: "claim",
                newName: "Claim");

            migrationBuilder.RenameIndex(
                name: "IX_claim_LecturerID",
                table: "Claim",
                newName: "IX_Claim_LecturerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claim",
                table: "Claim",
                column: "ClaimID");

            migrationBuilder.AddForeignKey(
                name: "FK_Claim_lecturer_LecturerID",
                table: "Claim",
                column: "LecturerID",
                principalTable: "lecturer",
                principalColumn: "LecturerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claim_lecturer_LecturerID",
                table: "Claim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Claim",
                table: "Claim");

            migrationBuilder.RenameTable(
                name: "Claim",
                newName: "claim");

            migrationBuilder.RenameIndex(
                name: "IX_Claim_LecturerID",
                table: "claim",
                newName: "IX_claim_LecturerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_claim",
                table: "claim",
                column: "ClaimID");

            migrationBuilder.AddForeignKey(
                name: "FK_claim_lecturer_LecturerID",
                table: "claim",
                column: "LecturerID",
                principalTable: "lecturer",
                principalColumn: "LecturerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
