using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyRequestTechnicians_technicians_TechnicianId",
                table: "EmergencyRequestTechnicians");

            migrationBuilder.DropForeignKey(
                name: "FK_technicians_AspNetUsers_ApplicationUserId",
                table: "technicians");

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyRequestTechnicians_technicians_TechnicianId",
                table: "EmergencyRequestTechnicians",
                column: "TechnicianId",
                principalTable: "technicians",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_technicians_AspNetUsers_ApplicationUserId",
                table: "technicians",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyRequestTechnicians_technicians_TechnicianId",
                table: "EmergencyRequestTechnicians");

            migrationBuilder.DropForeignKey(
                name: "FK_technicians_AspNetUsers_ApplicationUserId",
                table: "technicians");

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyRequestTechnicians_technicians_TechnicianId",
                table: "EmergencyRequestTechnicians",
                column: "TechnicianId",
                principalTable: "technicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_technicians_AspNetUsers_ApplicationUserId",
                table: "technicians",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
