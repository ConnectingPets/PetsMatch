using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedConversationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "Messages");

            migrationBuilder.AddColumn<Guid>(
                name: "MessageId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "message id");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "message match id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AnimalId",
                table: "Messages",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MatchId",
                table: "Messages",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Matches_MatchId",
                table: "Messages",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Matches_MatchId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AnimalId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MatchId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Messages");
        }
    }
}
