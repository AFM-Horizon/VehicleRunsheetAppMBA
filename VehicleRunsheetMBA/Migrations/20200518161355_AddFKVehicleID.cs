using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleRunsheetMBAProj.Migrations
{
    public partial class AddFKVehicleID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Runsheets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Runsheets");
        }
    }
}
