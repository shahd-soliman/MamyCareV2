using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamyCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGovernorateHospitalpropertyToHospital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_GovernorateId",
                table: "Hospitals",
                column: "GovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_GovernorateHospitals_GovernorateId",
                table: "Hospitals",
                column: "GovernorateId",
                principalTable: "GovernorateHospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_GovernorateHospitals_GovernorateId",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_GovernorateId",
                table: "Hospitals");
        }
    }
}
