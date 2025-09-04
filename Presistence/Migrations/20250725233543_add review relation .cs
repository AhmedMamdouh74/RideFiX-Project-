using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    /// <inheritdoc />
    public partial class addreviewrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmergencyRequestId",
                table: "reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "emergencyRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_EmergencyRequestId",
                table: "reviews",
                column: "EmergencyRequestId",
                unique: true,
                filter: "[EmergencyRequestId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_emergencyRequests_EmergencyRequestId",
                table: "reviews",
                column: "EmergencyRequestId",
                principalTable: "emergencyRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_emergencyRequests_EmergencyRequestId",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_EmergencyRequestId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "EmergencyRequestId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "emergencyRequests");
        }
    }
}
