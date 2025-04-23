using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamyCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBabyImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

            migrationBuilder.AddColumn<string>(
                name: "MemoriesPicsUrls",
                table: "Babies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicUrl",
                table: "Babies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
       

            migrationBuilder.DropColumn(
                name: "MemoriesPicsUrls",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "ProfilePicUrl",
                table: "Babies");
        }
    }
}
