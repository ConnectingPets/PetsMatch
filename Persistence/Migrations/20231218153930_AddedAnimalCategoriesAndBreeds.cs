using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedAnimalCategoriesAndBreeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] { "AnimalCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Dog" },
                    { 2, "Cat" },
                    { 3, "Rabbit" }
                });

            migrationBuilder.InsertData(
                table: "Breeds",
                columns: new[] { "BreedId", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Persian Cat" },
                    { 2, 2, "Bengal Cat" },
                    { 3, 3, "American Rabbit" },
                    { 4, 1, "Pitbull" },
                    { 5, 1, "BullDog" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "BreedId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "BreedId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "BreedId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "BreedId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Breeds",
                keyColumn: "BreedId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AnimalCategories",
                keyColumn: "AnimalCategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AnimalCategories",
                keyColumn: "AnimalCategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AnimalCategories",
                keyColumn: "AnimalCategoryId",
                keyValue: 3);
        }
    }
}
