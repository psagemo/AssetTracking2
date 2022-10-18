using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracking2.Migrations
{
    public partial class IdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MobilePhones",
                newName: "MobilePhoneId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Laptops",
                newName: "LaptopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MobilePhoneId",
                table: "MobilePhones",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LaptopId",
                table: "Laptops",
                newName: "Id");
        }
    }
}
