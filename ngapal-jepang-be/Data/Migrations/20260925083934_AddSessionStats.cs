using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngapal_jepang_be.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectCount",
                table: "LearnSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MissCount",
                table: "LearnSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectCount",
                table: "LearnSessions");

            migrationBuilder.DropColumn(
                name: "MissCount",
                table: "LearnSessions");
        }
    }
}
