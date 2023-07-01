using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    /// <inheritdoc />
    public partial class NewSkillField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MPCost",
                table: "Skills",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1,
                column: "MPCost",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2,
                column: "MPCost",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                column: "MPCost",
                value: 16);

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "MPCost", "Name" },
                values: new object[] { 4, 50, 16, "Multiple shots" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "MPCost",
                table: "Skills");
        }
    }
}
