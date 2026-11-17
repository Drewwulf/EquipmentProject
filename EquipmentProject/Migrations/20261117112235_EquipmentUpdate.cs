using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentProject.Migrations
{
    /// <inheritdoc />
    public partial class EquipmentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YouTubeVideo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YouTubeVideo",
                table: "Products");
        }
    }
}
