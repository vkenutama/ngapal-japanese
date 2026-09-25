using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngapal_jepang_be.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddExpiredAtInLearnSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredAt",
                table: "LearnSessions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredAt",
                table: "LearnSessions");
        }
    }
}
