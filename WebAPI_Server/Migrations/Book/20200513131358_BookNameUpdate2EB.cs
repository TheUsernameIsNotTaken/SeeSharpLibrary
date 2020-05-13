using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI_Server.Migrations.Book
{
    public partial class BookNameUpdate2EB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Books");

            migrationBuilder.AlterColumn<decimal>(
                name: "ISBN",
                table: "Books",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Books");

            migrationBuilder.AlterColumn<long>(
                name: "ISBN",
                table: "Books",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<DateTime>(
                name: "Published",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
