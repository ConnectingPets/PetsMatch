using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedAnimalMatchTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Animals_AnimalOneId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Animals_AnimalTwoId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_AnimalTwoId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "AnimalOneId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "AnimalTwoId",
                table: "Matches");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "Matches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "match id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "MatchId");

            migrationBuilder.CreateTable(
                name: "AnimalMatches",
                columns: table => new
                {
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "animal id"),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "match id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalMatches", x => new { x.AnimalId, x.MatchId });
                    table.ForeignKey(
                        name: "FK_AnimalMatches_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId");
                    table.ForeignKey(
                        name: "FK_AnimalMatches_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId");
                },
                comment: "animal match table");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalMatches_MatchId",
                table: "AnimalMatches",
                column: "MatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalMatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Matches");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimalOneId",
                table: "Matches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "match animal one id");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimalTwoId",
                table: "Matches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "match animal two id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                columns: new[] { "AnimalOneId", "AnimalTwoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AnimalTwoId",
                table: "Matches",
                column: "AnimalTwoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Animals_AnimalOneId",
                table: "Matches",
                column: "AnimalOneId",
                principalTable: "Animals",
                principalColumn: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Animals_AnimalTwoId",
                table: "Matches",
                column: "AnimalTwoId",
                principalTable: "Animals",
                principalColumn: "AnimalId");
        }
    }
}
