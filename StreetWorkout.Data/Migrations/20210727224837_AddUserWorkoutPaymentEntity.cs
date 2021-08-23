namespace StreetWorkout.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddUserWorkoutPaymentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWorkoutPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupWorkoutId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Card = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkoutPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorkoutPayments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserWorkoutPayments_GroupWorkouts_GroupWorkoutId",
                        column: x => x.GroupWorkoutId,
                        principalTable: "GroupWorkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutPayments_GroupWorkoutId",
                table: "UserWorkoutPayments",
                column: "GroupWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutPayments_UserId",
                table: "UserWorkoutPayments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWorkoutPayments");
        }
    }
}
