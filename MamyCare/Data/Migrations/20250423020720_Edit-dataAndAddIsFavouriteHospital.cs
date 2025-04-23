using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamyCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditdataAndAddIsFavouriteHospital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Mothers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Mothers");

            migrationBuilder.DropColumn(
                name: "Isfavourite",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "Babies");

            migrationBuilder.AddColumn<bool>(
                name: "Isfavourite",
                table: "FavouriteHospitals",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isfavourite",
                table: "FavouriteHospitals");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Mothers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Mothers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Isfavourite",
                table: "Hospitals",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "Babies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "weight",
                table: "Babies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
