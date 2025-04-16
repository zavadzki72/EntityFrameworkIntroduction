using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infraestructure.Maps
{
    public class BookMap : BaseMap<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.PublicationYear)
                .IsRequired();

            builder.Property(x => x.Category)
                .IsRequired();

            builder.HasIndex(x => new { x.Title, x.PublicationYear }).IsUnique();
            builder.HasIndex(x => x.Title);

            base.Configure(builder);
        }
    }
}
