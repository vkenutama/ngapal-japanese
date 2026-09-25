using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngapal_jepang_be.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCardsQueue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
