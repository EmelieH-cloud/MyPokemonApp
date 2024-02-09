using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullstackPokemonApp.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Types_TypeId",
                table: "Pokemons");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Pokemons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Types_TypeId",
                table: "Pokemons",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Types_TypeId",
                table: "Pokemons");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Pokemons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Types_TypeId",
                table: "Pokemons",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
