using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI_Server.Migrations.Book
{
    public partial class bookIni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Borrowed = table.Column<bool>(nullable: false),
                    BorrowerName = table.Column<string>(maxLength: 100, nullable: true),
                    ReturnUntil = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
