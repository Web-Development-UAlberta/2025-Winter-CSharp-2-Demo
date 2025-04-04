using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNET_Console_Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "model",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    manufacturer_id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model", x => x.id);
                    table.ForeignKey(
                        name: "FK_model_manufacturer_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalTable: "manufacturer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vehicle",
                columns: table => new
                {
                    vin = table.Column<string>(type: "TEXT", nullable: false),
                    model_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle", x => x.vin);
                    table.ForeignKey(
                        name: "FK_vehicle_model_model_id",
                        column: x => x.model_id,
                        principalTable: "model",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_model_manufacturer_id",
                table: "model",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_model_id",
                table: "vehicle",
                column: "model_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicle");

            migrationBuilder.DropTable(
                name: "model");

            migrationBuilder.DropTable(
                name: "manufacturer");
        }
    }
}
