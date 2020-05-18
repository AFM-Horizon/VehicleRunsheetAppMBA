using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleRunsheetMBAProj.Migrations
{
    public partial class OneToManyVehicleDetails_Runsheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Runsheets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Runsheets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
