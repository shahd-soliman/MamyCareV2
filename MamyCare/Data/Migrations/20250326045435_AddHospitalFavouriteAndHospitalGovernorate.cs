using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamyCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHospitalFavouriteAndHospitalGovernorate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Governerate",
                table: "Hospitals");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Hospitals",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "GovernorateId",
                table: "Mothers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Hospitals",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Hospitals",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "GovernorateId",
                table: "Hospitals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FavouriteHospitals",
                columns: table => new
                {
                    motherId = table.Column<int>(type: "int", nullable: false),
                    hospitalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteHospitals", x => new { x.motherId, x.hospitalId });
                    table.ForeignKey(
                        name: "FK_FavouriteHospitals_Hospitals_hospitalId",
                        column: x => x.hospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavouriteHospitals_Mothers_motherId",
                        column: x => x.motherId,
                        principalTable: "Mothers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GovernorateHospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernorateHospitals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteHospitals_hospitalId",
                table: "FavouriteHospitals",
                column: "hospitalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteHospitals");

            migrationBuilder.DropTable(
                name: "GovernorateHospitals");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                table: "Mothers");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                table: "Hospitals");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Hospitals",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Governerate",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
