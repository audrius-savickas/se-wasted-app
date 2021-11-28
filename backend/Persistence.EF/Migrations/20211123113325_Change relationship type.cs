using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.EF.Migrations
{
    public partial class Changerelationshiptype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypesOfFood_Foods_FoodEntityId",
                table: "TypesOfFood");

            migrationBuilder.DropIndex(
                name: "IX_TypesOfFood_FoodEntityId",
                table: "TypesOfFood");

            migrationBuilder.DropColumn(
                name: "FoodEntityId",
                table: "TypesOfFood");

            migrationBuilder.CreateTable(
                name: "FoodEntityTypeOfFoodEntity",
                columns: table => new
                {
                    FoodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypesOfFoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodEntityTypeOfFoodEntity", x => new { x.FoodsId, x.TypesOfFoodId });
                    table.ForeignKey(
                        name: "FK_FoodEntityTypeOfFoodEntity_Foods_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodEntityTypeOfFoodEntity_TypesOfFood_TypesOfFoodId",
                        column: x => x.TypesOfFoodId,
                        principalTable: "TypesOfFood",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntityTypeOfFoodEntity_TypesOfFoodId",
                table: "FoodEntityTypeOfFoodEntity",
                column: "TypesOfFoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodEntityTypeOfFoodEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "FoodEntityId",
                table: "TypesOfFood",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypesOfFood_FoodEntityId",
                table: "TypesOfFood",
                column: "FoodEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypesOfFood_Foods_FoodEntityId",
                table: "TypesOfFood",
                column: "FoodEntityId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
