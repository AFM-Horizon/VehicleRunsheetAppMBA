using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleRunsheetMBAProj.Migrations
{
    public partial class VehicleDetailsRunsheetRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleDetails",
                table: "Runsheets");

            migrationBuilder.AddColumn<int>(
                name: "VehicleDetailsId",
                table: "Runsheets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Runsheets_VehicleDetailsId",
                table: "Runsheets",
                column: "VehicleDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Runsheets_VehicleDetails_VehicleDetailsId",
                table: "Runsheets",
                column: "VehicleDetailsId",
                principalTable: "VehicleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runsheets_VehicleDetails_VehicleDetailsId",
                table: "Runsheets");

            migrationBuilder.DropIndex(
                name: "IX_Runsheets_VehicleDetailsId",
                table: "Runsheets");

            migrationBuilder.DropColumn(
                name: "VehicleDetailsId",
                table: "Runsheets");

            migrationBuilder.AddColumn<string>(
                name: "VehicleDetails",
                table: "Runsheets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
