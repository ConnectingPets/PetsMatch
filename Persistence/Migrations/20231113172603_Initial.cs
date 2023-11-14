using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalCategories",
                columns: table => new
                {
                    AnimalCategoryId = table.Column<int>(type: "int", nullable: false, comment: "animal category id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "animal category name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalCategories", x => x.AnimalCategoryId);
                },
                comment: "animal category table");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "user name"),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "user description"),
                    Age = table.Column<int>(type: "int", nullable: false, comment: "user age"),
                    Education = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "user education"),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true, comment: "user photo"),
                    JobTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "user job title"),
                    Gender = table.Column<int>(type: "int", nullable: false, comment: "user gender"),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "user address"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "user city"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                },
                comment: "user table");

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "conversation id"),
                    StartedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "timestamp when the conversation started")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.ConversationId);
                },
                comment: "conversation table");

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
                name: "Breeds",
                columns: table => new
                {
                    BreedId = table.Column<int>(type: "int", nullable: false, comment: "breed id")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "breed name"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "animal category id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.BreedId);
                    table.ForeignKey(
                        name: "FK_Breeds_AnimalCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AnimalCategories",
                        principalColumn: "AnimalCategoryId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "breed table");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "animal id"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "animal name"),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "animal description"),
                    Age = table.Column<int>(type: "int", nullable: false, comment: "animal age"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "animal birth date"),
                    IsEducated = table.Column<bool>(type: "bit", nullable: false, comment: "stores if the animal is educated"),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "animal photo"),
                    HealthStatus = table.Column<int>(type: "int", nullable: false, comment: "animal health status"),
                    Gender = table.Column<int>(type: "int", nullable: false, comment: "animal gender"),
                    SocialMedia = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "animal social media"),
                    Weight = table.Column<double>(type: "float", nullable: true, comment: "animal weight"),
                    IsHavingValidDocuments = table.Column<bool>(type: "bit", nullable: false, comment: "it stores if the animal has valid documents"),
                    BreedId = table.Column<int>(type: "int", nullable: false, comment: "animal breed id"),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "animal owner id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.AnimalId);
                    table.ForeignKey(
                        name: "FK_Animals_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animals_Breeds_BreedId",
                        column: x => x.BreedId,
                        principalTable: "Breeds",
                        principalColumn: "BreedId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "animal table");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    AnimalOneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "match animal one id"),
                    AnimalTwoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "match animal two id"),
                    MatchOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "timestamp when the match is done")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => new { x.AnimalOneId, x.AnimalTwoId });
                    table.ForeignKey(
                        name: "FK_Matches_Animals_AnimalOneId",
                        column: x => x.AnimalOneId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId");
                    table.ForeignKey(
                        name: "FK_Matches_Animals_AnimalTwoId",
                        column: x => x.AnimalTwoId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId");
                },
                comment: "match table");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "message animal id"),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "message conversation id"),
                    Content = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false, comment: "message content"),
                    SentOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "timestamp when the message is sent")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => new { x.AnimalId, x.ConversationId });
                    table.ForeignKey(
                        name: "FK_Messages_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId");
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "ConversationId");
                },
                comment: "message table");

            migrationBuilder.CreateTable(
                name: "Swipes",
                columns: table => new
                {
                    SwiperAnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "swiper animal id"),
                    SwipeeAnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "swipee animal id"),
                    SwipedRight = table.Column<bool>(type: "bit", nullable: false, comment: "it stores of the swipe is right"),
                    SwipedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "timestamp when the swipe is made")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Swipes", x => new { x.SwiperAnimalId, x.SwipeeAnimalId });
                    table.ForeignKey(
                        name: "FK_Swipes_Animals_SwipeeAnimalId",
                        column: x => x.SwipeeAnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId");
                    table.ForeignKey(
                        name: "FK_Swipes_Animals_SwiperAnimalId",
                        column: x => x.SwiperAnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId");
                },
                comment: "swipe table");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_BreedId",
                table: "Animals",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_OwnerId",
                table: "Animals",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Breeds_CategoryId",
                table: "Breeds",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AnimalTwoId",
                table: "Matches",
                column: "AnimalTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Swipes_SwipeeAnimalId",
                table: "Swipes",
                column: "SwipeeAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPassions_UserId",
                table: "UsersPassions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Swipes");

            migrationBuilder.DropTable(
                name: "UsersPassions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Passions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Breeds");

            migrationBuilder.DropTable(
                name: "AnimalCategories");
        }
    }
}
