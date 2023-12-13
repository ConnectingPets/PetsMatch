using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLastModifiedFieldsToAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Animals");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedBreed",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "animal last modified breed");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedGender",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "animal last modified gender");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedName",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "animal last modified name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedBreed",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "LastModifiedGender",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "LastModifiedName",
                table: "Animals");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "animal last modified");
        }
    }
}
