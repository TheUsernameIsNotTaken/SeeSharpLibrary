using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI_Server.Migrations.Book
{
    public partial class BookISBNtoCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Books",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Books");

            migrationBuilder.AddColumn<decimal>(
                name: "ISBN",
                table: "Books",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
