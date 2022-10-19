using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracking2.Migrations
{
    public partial class AssetCenteredData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_Offices_OfficeId",
                table: "Laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhones_Offices_OfficeId",
                table: "MobilePhones");

            migrationBuilder.DropIndex(
                name: "IX_MobilePhones_OfficeId",
                table: "MobilePhones");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_OfficeId",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "MobilePhones");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "MobilePhones");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "MobilePhones");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Laptops");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Assets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Assets");

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "MobilePhones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "MobilePhones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "MobilePhones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Laptops",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhones_OfficeId",
                table: "MobilePhones",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_OfficeId",
                table: "Laptops",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_Offices_OfficeId",
                table: "Laptops",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhones_Offices_OfficeId",
                table: "MobilePhones",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
