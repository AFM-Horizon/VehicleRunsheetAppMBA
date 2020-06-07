using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleRunsheetMBAProj.Migrations
{
    public partial class VehicleIdNavPropertyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runsheets_VehicleDetails_VehicleDetailsId",
                table: "Runsheets");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleDetailsId",
                table: "Runsheets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Runsheets_VehicleDetails_VehicleDetailsId",
                table: "Runsheets",
                column: "VehicleDetailsId",
                principalTable: "VehicleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runsheets_VehicleDetails_VehicleDetailsId",
                table: "Runsheets");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleDetailsId",
                table: "Runsheets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Runsheets_VehicleDetails_VehicleDetailsId",
                table: "Runsheets",
                column: "VehicleDetailsId",
                principalTable: "VehicleDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
