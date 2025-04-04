using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNET_Console_Application.Migrations
{
    /// <inheritdoc />
    public partial class Odometer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "odometer",
                table: "vehicle",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "odometer",
                table: "vehicle");
        }
    }
}
