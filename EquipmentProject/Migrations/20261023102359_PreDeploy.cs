using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentProject.Migrations
{
    /// <inheritdoc />
    public partial class PreDeploy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubcategoryId",
                table: "Categories",
                newName: "SubCategoryID");

            migrationBuilder.AlterColumn<string>(
                name: "FullDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImgPath",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "IdentityPasskeyData",
                columns: table => new
                {
                    PublicKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SignCount = table.Column<long>(type: "bigint", nullable: false),
                    Transports = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUserVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsBackupEligible = table.Column<bool>(type: "bit", nullable: false),
                    IsBackedUp = table.Column<bool>(type: "bit", nullable: false),
                    AttestationObject = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ClientDataJson = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubHeaderInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialFacebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialInstagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialTelegram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgPathForLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgPathForHeader = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Schedule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteSettingsId = table.Column<int>(type: "int", nullable: false),
                    isdeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_SiteSettings_SiteSettingsId",
                        column: x => x.SiteSettingsId,
                        principalTable: "SiteSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WhyWe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subheader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    sitesettingId = table.Column<int>(type: "int", nullable: false),
                    SiteSettingsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhyWe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WhyWe_SiteSettings_SiteSettingsId",
                        column: x => x.SiteSettingsId,
                        principalTable: "SiteSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "HeaderInfo", "ImgPath", "ImgPathForHeader", "ImgPathForLogo", "ShopDesc", "ShopName", "SocialFacebook", "SocialInstagram", "SocialTelegram", "SubHeaderInfo" },
                values: new object[] { 1, "Header Info", null, null, null, "Shop Description", "shop", "", "", "", "SubHeader Info" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "Email", "PhoneNumber", "Schedule", "SiteSettingsId", "isdeleted" },
                values: new object[] { 1, "Adress", "Compane@company.com", "+380123456789", "Schedule", 1, false });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SiteSettingsId",
                table: "Contacts",
                column: "SiteSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_WhyWe_SiteSettingsId",
                table: "WhyWe",
                column: "SiteSettingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "IdentityPasskeyData");

            migrationBuilder.DropTable(
                name: "WhyWe");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.RenameColumn(
                name: "SubCategoryID",
                table: "Categories",
                newName: "SubcategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "FullDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImgPath",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
