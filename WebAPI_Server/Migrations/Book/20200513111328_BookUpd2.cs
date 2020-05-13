using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI_Server.Migrations.Book
{
    public partial class BookUpd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ISBN",
                table: "Books",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ISBN",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
