using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseManagementSystem1.Migrations
{
    public partial class FriendsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Friends",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Friends",
                newName: "FriendsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Friends",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FriendsId",
                table: "Friends",
                newName: "Email");
        }
    }
}
