
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class ExcerptTextConfiguration : IEntityTypeConfiguration<ExcerptText>
{
    public void Configure(EntityTypeBuilder<ExcerptText> builder)
    {

        builder.Property(p => p.Text)
            .IsRequired();

        ////Indexes
        //builder.HasIndex(p => p.EmailAddress)
        //    .IsUnique()
        //    .HasDatabaseName("ExcerptTextEmailAddress");
    }
}
