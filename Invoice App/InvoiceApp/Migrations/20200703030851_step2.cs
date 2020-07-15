using Microsoft.EntityFrameworkCore.Migrations;

namespace WasmahTask.Migrations
{
    public partial class step2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AvailableQuantity", "Description", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { -1, 10, "Made by Apple company", "IPhone X", 23000f },
                    { -2, 8, "Made by Samsung company", "Samsung Note 8", 12000f },
                    { -3, 25, "Made by Oppo company", "Oppo A5", 4000f },
                    { -4, 12, "Made by Hawaui company", "Hawaui E9", 6500f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
