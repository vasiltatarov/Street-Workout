namespace StreetWorkout.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddGroupWorkoutsRestriction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupWorkouts_Sports_SportId",
                table: "GroupWorkouts");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupWorkouts_Sports_SportId",
                table: "GroupWorkouts",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupWorkouts_Sports_SportId",
                table: "GroupWorkouts");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupWorkouts_Sports_SportId",
                table: "GroupWorkouts",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
