using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI_Server.Migrations.Book
{
    public partial class BookNameUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowerName",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "BorrowerFirstName",
                table: "Books",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorrowerLastName",
                table: "Books",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Published",
                table: "Books",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowerFirstName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BorrowerLastName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "BorrowerName",
                table: "Books",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
