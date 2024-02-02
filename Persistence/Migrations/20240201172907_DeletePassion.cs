using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeletePassion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersPassions");

            migrationBuilder.DropTable(
                name: "Passions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Passions",
                columns: table => new
                {
                    PassionId = table.Column<int>(type: "int", nullable: false, comment: "passion id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "passion name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passions", x => x.PassionId);
                },
                comment: "passion table");

            migrationBuilder.CreateTable(
                name: "UsersPassions",
                columns: table => new
                {
                    PassionId = table.Column<int>(type: "int", nullable: false, comment: "passion id"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "user id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPassions", x => new { x.PassionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersPassions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersPassions_Passions_PassionId",
                        column: x => x.PassionId,
                        principalTable: "Passions",
                        principalColumn: "PassionId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "user passion table");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPassions_UserId",
                table: "UsersPassions",
                column: "UserId");
        }
    }
}
