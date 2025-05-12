using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.Entities;
#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<int>(type: "INTEGER", nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            // string passwordHash = BCrypt.Net.BCrypt.HashPassword("test");

            // migrationBuilder.InsertData(
            //     table: "Users",
            //     columns: new[] { "Id", "FirstName", "LastName", "Username", "Role", "PasswordHash" },
            //     values: new object[,]
            //     {
            //         { 1, "Test", "User", "test", Role.Super, passwordHash }
            //     });
            
            migrationBuilder.CreateTable(
                 name: "Products",
                 columns: table => new
                 {
                     ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                      .Annotation("Sqlite:Autoincrement", true),
                     Name = table.Column<string>(type: "TEXT", nullable: true),
                     ProductCategory = table.Column<int>(type: "INTEGER", nullable: true),
                     Description = table.Column<string>(type: "TEXT", nullable: true),
                     Price = table.Column<decimal>(type: "INTEGER", nullable: true),
                     SKU = table.Column<string>(type: "TEXT", nullable: true),
                     Code = table.Column<string>(type: "TEXT", nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Products", x => x.ProductId);
                 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
