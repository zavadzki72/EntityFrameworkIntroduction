using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infraestructure.Maps
{
    public class AuthorMap : BaseMap<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(x => x.Books)
                .WithMany(x => x.Authors);

            builder.HasIndex(x => x.Name).IsUnique();

            base.Configure(builder);
        }
    }
}
