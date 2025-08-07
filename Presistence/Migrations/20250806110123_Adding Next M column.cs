using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingNextMcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_carOwners_OwnerId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarMaintenanceRecord_Car_CarId",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_CarMaintenanceRecord_MaintenanceTypes_MaintenanceTypeId",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarMaintenanceRecord",
                table: "CarMaintenanceRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "CarMaintenanceRecord",
                newName: "carMaintenanceRecords");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "cars");

            migrationBuilder.RenameIndex(
                name: "IX_CarMaintenanceRecord_MaintenanceTypeId",
                table: "carMaintenanceRecords",
                newName: "IX_carMaintenanceRecords_MaintenanceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CarMaintenanceRecord_CarId",
                table: "carMaintenanceRecords",
                newName: "IX_carMaintenanceRecords_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_OwnerId",
                table: "cars",
                newName: "IX_cars_OwnerId");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextMaintenanceDue",
                table: "carMaintenanceRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_carMaintenanceRecords",
                table: "carMaintenanceRecords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cars",
                table: "cars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_carMaintenanceRecords_MaintenanceTypes_MaintenanceTypeId",
                table: "carMaintenanceRecords",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carMaintenanceRecords_cars_CarId",
                table: "carMaintenanceRecords",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cars_carOwners_OwnerId",
                table: "cars",
                column: "OwnerId",
                principalTable: "carOwners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carMaintenanceRecords_MaintenanceTypes_MaintenanceTypeId",
                table: "carMaintenanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_carMaintenanceRecords_cars_CarId",
                table: "carMaintenanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_cars_carOwners_OwnerId",
                table: "cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cars",
                table: "cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_carMaintenanceRecords",
                table: "carMaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "NextMaintenanceDue",
                table: "carMaintenanceRecords");

            migrationBuilder.RenameTable(
                name: "cars",
                newName: "Car");

            migrationBuilder.RenameTable(
                name: "carMaintenanceRecords",
                newName: "CarMaintenanceRecord");

            migrationBuilder.RenameIndex(
                name: "IX_cars_OwnerId",
                table: "Car",
                newName: "IX_Car_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_carMaintenanceRecords_MaintenanceTypeId",
                table: "CarMaintenanceRecord",
                newName: "IX_CarMaintenanceRecord_MaintenanceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_carMaintenanceRecords_CarId",
                table: "CarMaintenanceRecord",
                newName: "IX_CarMaintenanceRecord_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarMaintenanceRecord",
                table: "CarMaintenanceRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_carOwners_OwnerId",
                table: "Car",
                column: "OwnerId",
                principalTable: "carOwners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarMaintenanceRecord_Car_CarId",
                table: "CarMaintenanceRecord",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarMaintenanceRecord_MaintenanceTypes_MaintenanceTypeId",
                table: "CarMaintenanceRecord",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
