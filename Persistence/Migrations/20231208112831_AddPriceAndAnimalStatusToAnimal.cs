using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceAndAnimalStatusToAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalStatus",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "animal status");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Animals",
                type: "decimal(18,2)",
                nullable: true,
                comment: "animal price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalStatus",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Animals");
        }
    }
}
