using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregunta_Proyectoid_Proyecto",
                schema: "dbo",
                table: "Pregunta");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Preguntaid_Pregunta",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_Preguntaid",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Pregunta_Proyectoid",
                schema: "dbo",
                table: "Pregunta");

            migrationBuilder.DropColumn(
                name: "Proyectoid",
                schema: "dbo",
                table: "Pregunta");

            migrationBuilder.AddColumn<Guid>(
                name: "Proyectoid",
                schema: "dbo",
                table: "Respuesta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProyectoidNavigationId",
                schema: "dbo",
                table: "Pregunta",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_Proyectoid",
                schema: "dbo",
                table: "Respuesta",
                column: "Proyectoid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Proyectoid_Proyecto",
                schema: "dbo",
                table: "Respuesta",
                column: "Proyectoid",
                principalSchema: "dbo",
                principalTable: "Pregunta",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pregunta_Proyecto_ProyectoidNavigationId",
                schema: "dbo",
                table: "Pregunta");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Proyectoid_Proyecto",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_Proyectoid",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Pregunta_ProyectoidNavigationId",
                schema: "dbo",
                table: "Pregunta");

            migrationBuilder.DropColumn(
                name: "Proyectoid",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.DropColumn(
                name: "ProyectoidNavigationId",
                schema: "dbo",
                table: "Pregunta");

            migrationBuilder.AddColumn<Guid>(
                name: "Proyectoid",
                schema: "dbo",
                table: "Pregunta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_Preguntaid",
                schema: "dbo",
                table: "Respuesta",
                column: "Preguntaid");

            migrationBuilder.CreateIndex(
                name: "IX_Pregunta_Proyectoid",
                schema: "dbo",
                table: "Pregunta",
                column: "Proyectoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Pregunta_Proyectoid_Proyecto",
                schema: "dbo",
                table: "Pregunta",
                column: "Proyectoid",
                principalSchema: "dbo",
                principalTable: "Proyecto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Preguntaid_Pregunta",
                schema: "dbo",
                table: "Respuesta",
                column: "Preguntaid",
                principalSchema: "dbo",
                principalTable: "Pregunta",
                principalColumn: "Id");
        }
    }
}
