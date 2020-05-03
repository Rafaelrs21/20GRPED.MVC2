using Microsoft.EntityFrameworkCore.Migrations;

namespace _20GRPED.MVC2.Data.Migrations
{
    public partial class Validacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPropertyTest",
                table: "Livros");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Livros",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Livros",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Paginas",
                table: "Livros",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paginas",
                table: "Livros");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "NewPropertyTest",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
