using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamyCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRateToHospital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IActive",
                table: "Hospitals",
                newName: "Isopened");

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Hospitals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Hospitals");

            migrationBuilder.RenameColumn(
                name: "Isopened",
                table: "Hospitals",
                newName: "IActive");
        }
    }
}
