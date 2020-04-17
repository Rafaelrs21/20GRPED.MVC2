using _20GRPED.MVC2.A02.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.A02.Data.Context
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext (DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<LivroEntity> Livros { get; set; }
    }
}
