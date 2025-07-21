using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processingresult",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Proyectoid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Suggestedsolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Benefitcalculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accompanyingstrategy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processingresult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processingresult_Proyectoid_Proyecto",
                        column: x => x.Proyectoid,
                        principalSchema: "dbo",
                        principalTable: "Proyecto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prompt",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Promtuser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Promtsystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Folder = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prompt", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processingresult_Proyectoid",
                schema: "dbo",
                table: "Processingresult",
                column: "Proyectoid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Processingresult",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Prompt",
                schema: "dbo");
        }
    }
}
