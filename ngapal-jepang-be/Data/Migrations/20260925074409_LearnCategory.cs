using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngapal_jepang_be.Data.Migrations
{
    /// <inheritdoc />
    public partial class LearnCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RetentionCategory",
                table: "LearnSessions",
                newName: "LearnCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LearnCategory",
                table: "LearnSessions",
                newName: "RetentionCategory");
        }
    }
}
