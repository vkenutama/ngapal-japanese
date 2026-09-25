using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngapal_jepang_be.Data.Migrations
{
    /// <inheritdoc />
    public partial class FlashcardAddDueDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ListenDueDate",
                table: "Flashcards",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OutputDueDate",
                table: "Flashcards",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadDueDate",
                table: "Flashcards",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListenDueDate",
                table: "Flashcards");

            migrationBuilder.DropColumn(
                name: "OutputDueDate",
                table: "Flashcards");

            migrationBuilder.DropColumn(
                name: "ReadDueDate",
                table: "Flashcards");
        }
    }
}
