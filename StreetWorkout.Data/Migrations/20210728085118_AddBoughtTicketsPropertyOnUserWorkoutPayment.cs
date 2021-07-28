namespace StreetWorkout.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddBoughtTicketsPropertyOnUserWorkoutPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "BoughtTickets",
                table: "UserWorkoutPayments",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoughtTickets",
                table: "UserWorkoutPayments");
        }
    }
}
