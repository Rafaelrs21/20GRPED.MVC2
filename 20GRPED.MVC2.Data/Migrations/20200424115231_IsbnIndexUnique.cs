using Microsoft.EntityFrameworkCore.Migrations;

namespace _20GRPED.MVC2.Data.Migrations
{
    public partial class IsbnIndexUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Livros_Isbn",
                table: "Livros",
                column: "Isbn",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Livros_Isbn",
                table: "Livros");
        }
    }
}
