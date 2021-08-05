namespace StreetWorkout.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddQuantityPropertyToSupplement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Quantity",
                table: "Supplements",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Supplements");
        }
    }
}
