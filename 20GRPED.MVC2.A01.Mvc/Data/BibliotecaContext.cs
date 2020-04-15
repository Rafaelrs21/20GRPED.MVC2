using _20GRPED.MVC1.A15.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC1.A15.Mvc.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext (DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<LivroModel> Livros { get; set; }
    }
}
