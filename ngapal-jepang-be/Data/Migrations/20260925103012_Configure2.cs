using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngapal_jepang_be.Data.Migrations
{
    /// <inheritdoc />
    public partial class Configure2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_LearnSessions_LearnSessionId",
                table: "Flashcards");

            migrationBuilder.DropIndex(
                name: "IX_Flashcards_LearnSessionId",
                table: "Flashcards");

            migrationBuilder.DropColumn(
                name: "LearnSessionId",
                table: "Flashcards");

            migrationBuilder.CreateTable(
                name: "FlashcardLearnSession",
                columns: table => new
                {
                    FlashcardQueueId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LearnSessionsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardLearnSession", x => new { x.FlashcardQueueId, x.LearnSessionsId });
                    table.ForeignKey(
                        name: "FK_FlashcardLearnSession_Flashcards_FlashcardQueueId",
                        column: x => x.FlashcardQueueId,
                        principalTable: "Flashcards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlashcardLearnSession_LearnSessions_LearnSessionsId",
                        column: x => x.LearnSessionsId,
                        principalTable: "LearnSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardLearnSession_LearnSessionsId",
                table: "FlashcardLearnSession",
                column: "LearnSessionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashcardLearnSession");

            migrationBuilder.AddColumn<Guid>(
                name: "LearnSessionId",
                table: "Flashcards",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flashcards_LearnSessionId",
                table: "Flashcards",
                column: "LearnSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_LearnSessions_LearnSessionId",
                table: "Flashcards",
                column: "LearnSessionId",
                principalTable: "LearnSessions",
                principalColumn: "Id");
        }
    }
}
