using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzaria.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    CPF = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "Pizzas",
                columns: table => new
                {
                    idPizza = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sabor = table.Column<string>(type: "TEXT", nullable: true),
                    Preco = table.Column<double>(type: "REAL", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.idPizza);
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    idVenda = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<double>(type: "REAL", nullable: true),
                    idCliente = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.idVenda);
                    table.ForeignKey(
                        name: "FK_Venda_Cliente_idCliente",
                        column: x => x.idCliente,
                        principalTable: "Cliente",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PizzasVenda",
                columns: table => new
                {
                    idPizza = table.Column<int>(type: "INTEGER", nullable: false),
                    idVenda = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: true),
                    Total = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzasVenda", x => new { x.idPizza, x.idVenda });
                    table.ForeignKey(
                        name: "FK_PizzasVenda_Pizzas_idPizza",
                        column: x => x.idPizza,
                        principalTable: "Pizzas",
                        principalColumn: "idPizza",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzasVenda_Venda_idVenda",
                        column: x => x.idVenda,
                        principalTable: "Venda",
                        principalColumn: "idVenda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_Sabor",
                table: "Pizzas",
                column: "Sabor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PizzasVenda_idVenda",
                table: "PizzasVenda",
                column: "idVenda");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_idCliente",
                table: "Venda",
                column: "idCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzasVenda");

            migrationBuilder.DropTable(
                name: "Pizzas");

            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
