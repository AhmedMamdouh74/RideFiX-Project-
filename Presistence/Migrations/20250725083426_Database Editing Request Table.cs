using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseEditingRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_emergencyRequests_technicians_TechnicainId",
                table: "emergencyRequests");

            migrationBuilder.DropIndex(
                name: "IX_emergencyRequests_TechnicainId",
                table: "emergencyRequests");

            migrationBuilder.DropColumn(
                name: "CallState",
                table: "emergencyRequests");

            migrationBuilder.DropColumn(
                name: "TechnicainId",
                table: "emergencyRequests");

            migrationBuilder.AddColumn<int>(
                name: "TechnicianId",
                table: "emergencyRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmergencyRequestTechnicians",
                columns: table => new
                {
                    TechnicianId = table.Column<int>(type: "int", nullable: false),
                    EmergencyRequestId = table.Column<int>(type: "int", nullable: false),
                    CallStatus = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyRequestTechnicians", x => new { x.EmergencyRequestId, x.TechnicianId });
                    table.ForeignKey(
                        name: "FK_EmergencyRequestTechnicians_emergencyRequests_EmergencyRequestId",
                        column: x => x.EmergencyRequestId,
                        principalTable: "emergencyRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmergencyRequestTechnicians_technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "technicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechReverseRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmergencyRequestId = table.Column<int>(type: "int", nullable: false),
                    TechnicianId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CallState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechReverseRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechReverseRequest_emergencyRequests_EmergencyRequestId",
                        column: x => x.EmergencyRequestId,
                        principalTable: "emergencyRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechReverseRequest_technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "technicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emergencyRequests_TechnicianId",
                table: "emergencyRequests",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyRequestTechnicians_TechnicianId",
                table: "EmergencyRequestTechnicians",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechReverseRequest_EmergencyRequestId",
                table: "TechReverseRequest",
                column: "EmergencyRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TechReverseRequest_TechnicianId",
                table: "TechReverseRequest",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_emergencyRequests_technicians_TechnicianId",
                table: "emergencyRequests",
                column: "TechnicianId",
                principalTable: "technicians",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_emergencyRequests_technicians_TechnicianId",
                table: "emergencyRequests");

            migrationBuilder.DropTable(
                name: "EmergencyRequestTechnicians");

            migrationBuilder.DropTable(
                name: "TechReverseRequest");

            migrationBuilder.DropIndex(
                name: "IX_emergencyRequests_TechnicianId",
                table: "emergencyRequests");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                table: "emergencyRequests");

            migrationBuilder.AddColumn<int>(
                name: "CallState",
                table: "emergencyRequests",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TechnicainId",
                table: "emergencyRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_emergencyRequests_TechnicainId",
                table: "emergencyRequests",
                column: "TechnicainId");

            migrationBuilder.AddForeignKey(
                name: "FK_emergencyRequests_technicians_TechnicainId",
                table: "emergencyRequests",
                column: "TechnicainId",
                principalTable: "technicians",
                principalColumn: "Id");
        }
    }
}
