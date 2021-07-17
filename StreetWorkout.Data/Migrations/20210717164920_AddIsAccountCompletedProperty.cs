using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetWorkout.Data.Migrations
{
    public partial class AddIsAccountCompletedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccountCompleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccountCompleted",
                table: "AspNetUsers");
        }
    }
}
