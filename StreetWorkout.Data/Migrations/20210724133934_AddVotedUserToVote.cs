namespace StreetWorkout.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddVotedUserToVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VotedUserId",
                table: "Votes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VotedUserId",
                table: "Votes",
                column: "VotedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_VotedUserId",
                table: "Votes",
                column: "VotedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_VotedUserId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_VotedUserId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "VotedUserId",
                table: "Votes");
        }
    }
}
