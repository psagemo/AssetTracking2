using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracking2.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_Assets_AssetId",
                table: "Laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhones_Assets_AssetId",
                table: "MobilePhones");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_MobilePhones_AssetId",
                table: "MobilePhones");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_AssetId",
                table: "Laptops");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "MobilePhones",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "MobilePhoneId",
                table: "MobilePhones",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "Laptops",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "LaptopId",
                table: "Laptops",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "MobilePhones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Laptops",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "MobilePhones");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Laptops");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "MobilePhones",
                newName: "AssetId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MobilePhones",
                newName: "MobilePhoneId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Laptops",
                newName: "AssetId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Laptops",
                newName: "LaptopId");

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhones_AssetId",
                table: "MobilePhones",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_AssetId",
                table: "Laptops",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_Assets_AssetId",
                table: "Laptops",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhones_Assets_AssetId",
                table: "MobilePhones",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
