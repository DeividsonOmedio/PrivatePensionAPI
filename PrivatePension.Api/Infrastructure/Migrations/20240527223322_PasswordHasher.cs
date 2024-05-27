using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PasswordHasher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEMvNY7s6kS2T8HnKi/yl+GSELUKIrTxU50ET30KscoqaHS1pOiDqqny8DjLI3P3pig==");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEC0IJzvxwP0gbDpkU0ddepJfBS/ZwlQK6fueapmL6sOLlyi2BilBKSWKGKw1AOHJAA==");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Available", "Description", "Name", "Price" },
                values: new object[] { false, "O Renda Fixa Crédito Privado II é um fundo que tem títulos de crédito privado selecionados pela BRAM ", "Crédito Privado II RF", 300m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Available", "Description", "Name", "Price" },
                values: new object[] { false, "Investir em títulos públicos e privados pós fixados, com rentabilidade atrelada ao CDI", "Crédito Privado Premium RF", 500m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "Admin@123");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "Client@123");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Descrição do Produto A", "Produto A", 100m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Descrição do Produto B", "Produto B", 200m });
        }
    }
}
