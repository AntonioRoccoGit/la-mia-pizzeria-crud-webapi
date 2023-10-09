using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    /// <inheritdoc />
    public partial class AddIngredientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pizzas_categories_category_id",
                table: "pizzas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pizzas",
                table: "pizzas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "pizzas",
                newName: "pizza");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "category");

            migrationBuilder.RenameIndex(
                name: "IX_pizzas_category_id",
                table: "pizza",
                newName: "IX_pizza_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pizza",
                table: "pizza",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_category",
                table: "category",
                column: "id");

            migrationBuilder.CreateTable(
                name: "ingredient",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientPizzaItem",
                columns: table => new
                {
                    PizzasPizzaItemId = table.Column<int>(type: "int", nullable: false),
                    ingredientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPizzaItem", x => new { x.PizzasPizzaItemId, x.ingredientsId });
                    table.ForeignKey(
                        name: "FK_IngredientPizzaItem_ingredient_ingredientsId",
                        column: x => x.ingredientsId,
                        principalTable: "ingredient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientPizzaItem_pizza_PizzasPizzaItemId",
                        column: x => x.PizzasPizzaItemId,
                        principalTable: "pizza",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPizzaItem_ingredientsId",
                table: "IngredientPizzaItem",
                column: "ingredientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_pizza_category_category_id",
                table: "pizza",
                column: "category_id",
                principalTable: "category",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pizza_category_category_id",
                table: "pizza");

            migrationBuilder.DropTable(
                name: "IngredientPizzaItem");

            migrationBuilder.DropTable(
                name: "ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pizza",
                table: "pizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_category",
                table: "category");

            migrationBuilder.RenameTable(
                name: "pizza",
                newName: "pizzas");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "categories");

            migrationBuilder.RenameIndex(
                name: "IX_pizza_category_id",
                table: "pizzas",
                newName: "IX_pizzas_category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pizzas",
                table: "pizzas",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_pizzas_categories_category_id",
                table: "pizzas",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");
        }
    }
}
