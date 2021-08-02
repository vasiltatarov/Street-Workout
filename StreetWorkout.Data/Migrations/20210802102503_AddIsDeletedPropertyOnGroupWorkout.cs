namespace StreetWorkout.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddIsDeletedPropertyOnGroupWorkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GroupWorkouts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GroupWorkouts");
        }
    }
}
