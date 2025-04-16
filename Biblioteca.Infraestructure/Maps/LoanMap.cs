using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infraestructure.Maps
{
    public class LoanMap : BaseMap<Loan>
    {
        public override void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.Property(x => x.Username)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.LoanDate)
                .IsRequired();

            builder.Property(x => x.ReturnDate)
                .IsRequired();

            builder.Property(x => x.LoanValue)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasOne(x => x.Book)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.BookId);

            builder.HasIndex(x => x.Username);

            base.Configure(builder);
        }
    }
}
