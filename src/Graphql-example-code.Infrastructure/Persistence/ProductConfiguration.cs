using Graphql_example_code.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Graphql_example_code.Infrastructure.Persistence;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product", "dbo");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
                .HasMaxLength(500)
                .IsRequired();
        builder.Property(x => x.Description)
                .HasMaxLength(1000)
                .IsRequired(false);
    }
}
