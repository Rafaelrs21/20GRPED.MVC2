using _20GRPED.MVC2.Data.Context.Configuration;
using _20GRPED.MVC2.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.Data.Context
{
    public class BibliotecaContext : DbContext
    {
        //add-migration AddedLivroEntity -context BibliotecaContext -StartupProject 20GRPED.MVC2.WebApi
        //update-database -context BibliotecaContext -StartupProject 20GRPED.MVC2.WebApi
        public BibliotecaContext (DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<LivroEntity> Livros { get; set; }
        public DbSet<AutorEntity> Autores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LivroConfiguration());
            modelBuilder.ApplyConfiguration(new AutorConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
