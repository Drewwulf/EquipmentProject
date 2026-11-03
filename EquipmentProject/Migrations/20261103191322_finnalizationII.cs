using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentProject.Migrations
{
    /// <inheritdoc />
    public partial class finnalizationII : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SocialTikTok",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialWhatsApp",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialYouTube",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteSettingsId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_SiteSettings_SiteSettingsId",
                        column: x => x.SiteSettingsId,
                        principalTable: "SiteSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SocialTikTok", "SocialWhatsApp", "SocialYouTube" },
                values: new object[] { "", "", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SiteSettingsId",
                table: "Images",
                column: "SiteSettingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropColumn(
                name: "SocialTikTok",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "SocialWhatsApp",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "SocialYouTube",
                table: "SiteSettings");
        }
    }
}
