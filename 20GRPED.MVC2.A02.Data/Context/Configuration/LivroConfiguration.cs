using _20GRPED.MVC2.A02.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _20GRPED.MVC2.A02.Data.Context.Configuration
{
    public class LivroConfiguration : IEntityTypeConfiguration<LivroEntity>
    {
        public void Configure(EntityTypeBuilder<LivroEntity> builder)
        {
            builder
                .HasIndex(x => x.Isbn)
                .IsUnique();
        }
    }
}
