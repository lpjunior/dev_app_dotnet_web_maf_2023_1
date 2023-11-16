using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentityMigrations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UsuarioModificou",
                table: "AspNetUsers",
                newName: "CreationUser");

            migrationBuilder.RenameColumn(
                name: "UsuarioCriou",
                table: "AspNetUsers",
                newName: "ChangedUser");

            migrationBuilder.RenameColumn(
                name: "DataModificacao",
                table: "AspNetUsers",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "AspNetUsers",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "AspNetUsers",
                newName: "DataModificacao");

            migrationBuilder.RenameColumn(
                name: "CreationUser",
                table: "AspNetUsers",
                newName: "UsuarioModificou");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AspNetUsers",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "ChangedUser",
                table: "AspNetUsers",
                newName: "UsuarioCriou");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
