using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "CarMaintenanceRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaintenanceCenter",
                table: "CarMaintenanceRecord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceCount",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMaintenanceCost",
                table: "Car",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfCar",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfFuel",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropColumn(
                name: "MaintenanceCenter",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropColumn(
                name: "MaintenanceCount",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "TotalMaintenanceCost",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "TypeOfCar",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "TypeOfFuel",
                table: "Car");
        }
    }
}
