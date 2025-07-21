using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationUpdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregunta_Proyecto_ProyectoidNavigationId",
                schema: "dbo",
                table: "Pregunta");

            migrationBuilder.DropIndex(
                name: "IX_Pregunta_ProyectoidNavigationId",
                schema: "dbo",
                table: "Pregunta");

            migrationBuilder.DropColumn(
                name: "ProyectoidNavigationId",
                schema: "dbo",
                table: "Pregunta");

            migrationBuilder.AddColumn<Guid>(
                name: "ProyectoidNavigationId",
                schema: "dbo",
                table: "Respuesta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_ProyectoidNavigationId",
                schema: "dbo",
                table: "Respuesta",
                column: "ProyectoidNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Proyecto_ProyectoidNavigationId",
                schema: "dbo",
                table: "Respuesta",
                column: "ProyectoidNavigationId",
                principalSchema: "dbo",
                principalTable: "Proyecto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Proyecto_ProyectoidNavigationId",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_ProyectoidNavigationId",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.DropColumn(
                name: "ProyectoidNavigationId",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.AddColumn<Guid>(
                name: "ProyectoidNavigationId",
                schema: "dbo",
                table: "Pregunta",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pregunta_ProyectoidNavigationId",
                schema: "dbo",
                table: "Pregunta",
                column: "ProyectoidNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pregunta_Proyecto_ProyectoidNavigationId",
                schema: "dbo",
                table: "Pregunta",
                column: "ProyectoidNavigationId",
                principalSchema: "dbo",
                principalTable: "Proyecto",
                principalColumn: "Id");
        }
    }
}
