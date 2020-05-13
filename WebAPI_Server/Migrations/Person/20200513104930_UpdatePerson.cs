using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI_Server.Migrations
{
    public partial class UpdatePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "People");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "People",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "People",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "People",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
