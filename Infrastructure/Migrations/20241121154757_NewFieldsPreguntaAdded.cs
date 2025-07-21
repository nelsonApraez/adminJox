using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldsPreguntaAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreCategoria",
                schema: "dbo",
                table: "Pregunta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroCategoria",
                schema: "dbo",
                table: "Pregunta",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreCategoria",
                schema: "dbo",
                table: "Pregunta");

            migrationBuilder.DropColumn(
                name: "NumeroCategoria",
                schema: "dbo",
                table: "Pregunta");
        }
    }
}
