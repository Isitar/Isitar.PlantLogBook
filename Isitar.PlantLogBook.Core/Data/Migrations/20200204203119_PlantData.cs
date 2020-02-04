using Microsoft.EntityFrameworkCore.Migrations;

namespace Isitar.PlantLogBook.Core.Data.Migrations
{
    public partial class PlantData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Plants",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlantState",
                table: "Plants",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "PlantState",
                table: "Plants");
        }
    }
}
