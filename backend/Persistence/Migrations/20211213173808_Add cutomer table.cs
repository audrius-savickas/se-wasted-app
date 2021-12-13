using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Addcutomertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerEntity",
                table: "CustomerEntity");

            migrationBuilder.RenameTable(
                name: "CustomerEntity",
                newName: "Customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "CustomerEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerEntity",
                table: "CustomerEntity",
                column: "Id");
        }
    }
}
