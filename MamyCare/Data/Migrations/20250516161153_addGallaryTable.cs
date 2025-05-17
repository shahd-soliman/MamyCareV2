using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamyCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class addGallaryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemoriesPicsUrls",
                table: "Babies");

            migrationBuilder.CreateTable(
                name: "Gallaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BabyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gallaries_Babies_BabyId",
                        column: x => x.BabyId,
                        principalTable: "Babies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gallaries_BabyId",
                table: "Gallaries",
                column: "BabyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gallaries");

            migrationBuilder.AddColumn<string>(
                name: "MemoriesPicsUrls",
                table: "Babies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
