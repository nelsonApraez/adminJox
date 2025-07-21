using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationUpdated3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Proyecto_ProyectoidNavigationId",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Proyectoid_Proyecto",
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

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_Preguntaid",
                schema: "dbo",
                table: "Respuesta",
                column: "Preguntaid");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Preguntaid_Pregunta",
                schema: "dbo",
                table: "Respuesta",
                column: "Preguntaid",
                principalSchema: "dbo",
                principalTable: "Pregunta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Proyectoid_Proyecto",
                schema: "dbo",
                table: "Respuesta",
                column: "Proyectoid",
                principalSchema: "dbo",
                principalTable: "Proyecto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Preguntaid_Pregunta",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuesta_Proyectoid_Proyecto",
                schema: "dbo",
                table: "Respuesta");

            migrationBuilder.DropIndex(
                name: "IX_Respuesta_Preguntaid",
                schema: "dbo",
                table: "Respuesta");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Respuesta_Proyectoid_Proyecto",
                schema: "dbo",
                table: "Respuesta",
                column: "Proyectoid",
                principalSchema: "dbo",
                principalTable: "Pregunta",
                principalColumn: "Id");
        }
    }
}
