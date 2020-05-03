using Microsoft.EntityFrameworkCore.Migrations;

namespace _20GRPED.MVC2.Data.Migrations
{
    public partial class AddedNewPropertyInLivroModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewPropertyTest",
                table: "Livros",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPropertyTest",
                table: "Livros");
        }
    }
}
