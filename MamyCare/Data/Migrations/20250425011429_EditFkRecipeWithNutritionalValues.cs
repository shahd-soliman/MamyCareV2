using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamyCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditFkRecipeWithNutritionalValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_NutritionalValues_NutritionalValuesId1",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_NutritionalValuesId1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "NutritionalValuesId1",
                table: "Recipes");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_NutritionalValuesId",
                table: "Recipes",
                column: "NutritionalValuesId",
                unique: true,
                filter: "[NutritionalValuesId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_NutritionalValues_NutritionalValuesId",
                table: "Recipes",
                column: "NutritionalValuesId",
                principalTable: "NutritionalValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_NutritionalValues_NutritionalValuesId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_NutritionalValuesId",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "NutritionalValuesId1",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_NutritionalValuesId1",
                table: "Recipes",
                column: "NutritionalValuesId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_NutritionalValues_NutritionalValuesId1",
                table: "Recipes",
                column: "NutritionalValuesId1",
                principalTable: "NutritionalValues",
                principalColumn: "Id");
        }
    }
}
