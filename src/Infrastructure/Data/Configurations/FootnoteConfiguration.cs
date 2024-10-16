
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class FootnoteConfiguration : IEntityTypeConfiguration<Footnote>
{
    public void Configure(EntityTypeBuilder<Footnote> builder)
    {

        builder.Property(p => p.Comment)
            .HasMaxLength(1000)
            .IsRequired();

        ////Indexes
        //builder.HasIndex(p => p.EmailAddress)
        //    .IsUnique()
        //    .HasDatabaseName("FootnoteEmailAddress");
    }
}
