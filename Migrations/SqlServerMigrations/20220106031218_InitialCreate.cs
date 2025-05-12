using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.Entities;

#nullable disable

namespace WebApi.Migrations.SqlServerMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            // string passwordHash = BCrypt.Net.BCrypt.HashPassword("test");

            // migrationBuilder.InsertData(
            //     table: "Users",
            //     columns: new[] { "Id", "FirstName", "LastName", "Username", "PasswordHash" },
            //     values: new object[,]
            //     {
            //         { 1, "Test", "User", "test", passwordHash }
            //     });
            migrationBuilder.CreateTable(
                 name: "Products",
                 columns: table => new
                 {
                     ProductId = table.Column<int>(type: "int", nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                     Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     ProductCategory = table.Column<int>(type: "int", nullable: true),
                     Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     Price = table.Column<decimal>(type: "decimal(5, 2)", nullable: true),
                     SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
