using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposUsuarioClaveEnEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clave",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Empleados");
        }
    }
}
