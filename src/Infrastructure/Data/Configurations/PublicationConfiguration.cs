
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class PublicationConfiguration : IEntityTypeConfiguration<Publication>
{
    public void Configure(EntityTypeBuilder<Publication> builder)
    {

        builder.Property(p => p.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.Url)
           .HasMaxLength(1000)
           .IsRequired();

        ////Indexes
        builder.HasIndex(p => p.SourceId)
            .HasDatabaseName("PublicationSourceId");
    }
}
