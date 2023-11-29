using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                comment: "animal last modified",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                comment: "animal created on",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "photo id"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "photo url"),
                    IsMain = table.Column<bool>(type: "bit", nullable: false, comment: "stores if the photo is main"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "photo user id"),
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "photo animal id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId");
                    table.ForeignKey(
                        name: "FK_Photos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                },
                comment: "photo table");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AnimalId",
                table: "Photos",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModified",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "animal last modified");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()",
                oldComment: "animal created on");
        }
    }
}
