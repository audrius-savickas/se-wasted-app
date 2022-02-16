using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Changefoodandreservationrelationshipto1tomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_FoodId",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FoodId",
                table: "Reservations",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_FoodId",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FoodId",
                table: "Reservations",
                column: "FoodId",
                unique: true);
        }
    }
}
