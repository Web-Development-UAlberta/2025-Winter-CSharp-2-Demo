using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotNET_Console_Application.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_model_manufacturer_manufacturer_id",
                table: "model");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_model_model_id",
                table: "vehicle");

            migrationBuilder.RenameIndex(
                name: "IX_vehicle_model_id",
                table: "vehicle",
                newName: "FK_Vehicle_Model");

            migrationBuilder.RenameIndex(
                name: "IX_model_manufacturer_id",
                table: "model",
                newName: "FK_Model_Manufacturer");

            migrationBuilder.InsertData(
                table: "manufacturer",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { -4, "Nissan" },
                    { -3, "Toyota" },
                    { -2, "Honda" },
                    { -1, "Mitsubishi" }
                });

            migrationBuilder.InsertData(
                table: "model",
                columns: new[] { "id", "manufacturer_id", "name" },
                values: new object[,]
                {
                    { -8, -4, "GTR" },
                    { -7, -4, "300ZX" },
                    { -6, -3, "MR2" },
                    { -5, -3, "Supra" },
                    { -4, -2, "S2000" },
                    { -3, -2, "NSX" },
                    { -2, -1, "3000GT" },
                    { -1, -1, "Eclipse" }
                });

            migrationBuilder.InsertData(
                table: "vehicle",
                columns: new[] { "vin", "model_id", "odometer" },
                values: new object[,]
                {
                    { "-----BCNR33004655", -8, 100 },
                    { "4A3AL54F3XE067712", -1, 100 },
                    { "JA3AN74K8XY001384", -2, 100 },
                    { "JH4NA1153MT000743", -3, 100 },
                    { "JHMAP21475S008443", -4, 100 },
                    { "JN1RZ24A1LX002317", -7, 100 },
                    { "JT2JA82J8S0028274", -5, 100 },
                    { "JTDFR320320052403", -6, 100 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Model_Manufacturer",
                table: "model",
                column: "manufacturer_id",
                principalTable: "manufacturer",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Model",
                table: "vehicle",
                column: "model_id",
                principalTable: "model",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_Manufacturer",
                table: "model");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Model",
                table: "vehicle");

            migrationBuilder.DeleteData(
                table: "vehicle",
                keyColumn: "vin",
                keyValue: "-----BCNR33004655");

            migrationBuilder.DeleteData(
                table: "vehicle",
                keyColumn: "vin",
                keyValue: "4A3AL54F3XE067712");

            migrationBuilder.DeleteData(
                table: "vehicle",
                keyColumn: "vin",
                keyValue: "JA3AN74K8XY001384");

            migrationBuilder.DeleteData(
                table: "vehicle",
                keyColumn: "vin",
                keyValue: "JH4NA1153MT000743");

            migrationBuilder.DeleteData(
                table: "vehicle",
                keyColumn: "vin",
                keyValue: "JHMAP21475S008443");

            migrationBuilder.DeleteData(
                table: "vehicle",
                keyColumn: "vin",
                keyValue: "JN1RZ24A1LX002317");

            migrationBuilder.DeleteData(
                table: "vehicle",
                keyColumn: "vin",
                keyValue: "JT2JA82J8S0028274");

            migrationBuilder.DeleteData(
                table: "vehicle",
                keyColumn: "vin",
                keyValue: "JTDFR320320052403");

            migrationBuilder.DeleteData(
                table: "model",
                keyColumn: "id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "model",
                keyColumn: "id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "model",
                keyColumn: "id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "model",
                keyColumn: "id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "model",
                keyColumn: "id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "model",
                keyColumn: "id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "model",
                keyColumn: "id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "model",
                keyColumn: "id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "manufacturer",
                keyColumn: "id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "manufacturer",
                keyColumn: "id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "manufacturer",
                keyColumn: "id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "manufacturer",
                keyColumn: "id",
                keyValue: -1);

            migrationBuilder.RenameIndex(
                name: "FK_Vehicle_Model",
                table: "vehicle",
                newName: "IX_vehicle_model_id");

            migrationBuilder.RenameIndex(
                name: "FK_Model_Manufacturer",
                table: "model",
                newName: "IX_model_manufacturer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_model_manufacturer_manufacturer_id",
                table: "model",
                column: "manufacturer_id",
                principalTable: "manufacturer",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_model_model_id",
                table: "vehicle",
                column: "model_id",
                principalTable: "model",
                principalColumn: "id");
        }
    }
}
