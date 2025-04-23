using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamyCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class addReminderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminder_Babies_BabyId",
                table: "Reminder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reminder",
                table: "Reminder");

            migrationBuilder.RenameTable(
                name: "Reminder",
                newName: "Reminders");

            migrationBuilder.RenameIndex(
                name: "IX_Reminder_BabyId",
                table: "Reminders",
                newName: "IX_Reminders_BabyId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reminders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reminders",
                table: "Reminders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Babies_BabyId",
                table: "Reminders",
                column: "BabyId",
                principalTable: "Babies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Babies_BabyId",
                table: "Reminders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reminders",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reminders");

            migrationBuilder.RenameTable(
                name: "Reminders",
                newName: "Reminder");

            migrationBuilder.RenameIndex(
                name: "IX_Reminders_BabyId",
                table: "Reminder",
                newName: "IX_Reminder_BabyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reminder",
                table: "Reminder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminder_Babies_BabyId",
                table: "Reminder",
                column: "BabyId",
                principalTable: "Babies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
