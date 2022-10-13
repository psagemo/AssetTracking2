using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracking2.Migrations
{
    public partial class ContextAsset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobilePhoneId = table.Column<int>(type: "int", nullable: true),
                    LaptopId = table.Column<int>(type: "int", nullable: true),
                    OfficeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Laptops_LaptopId",
                        column: x => x.LaptopId,
                        principalTable: "Laptops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assets_MobilePhones_MobilePhoneId",
                        column: x => x.MobilePhoneId,
                        principalTable: "MobilePhones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assets_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_LaptopId",
                table: "Assets",
                column: "LaptopId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_MobilePhoneId",
                table: "Assets",
                column: "MobilePhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OfficeId",
                table: "Assets",
                column: "OfficeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
