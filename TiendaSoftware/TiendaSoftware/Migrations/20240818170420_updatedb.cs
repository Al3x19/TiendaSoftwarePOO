using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Score",
                schema: "dbo",
                table: "reviews",
                newName: "score");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "score",
                schema: "dbo",
                table: "reviews",
                newName: "Score");
        }
    }
}
