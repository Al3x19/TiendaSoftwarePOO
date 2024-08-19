using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class imgs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagen",
                schema: "dbo",
                table: "software",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagen",
                schema: "dbo",
                table: "software");
        }
    }
}
