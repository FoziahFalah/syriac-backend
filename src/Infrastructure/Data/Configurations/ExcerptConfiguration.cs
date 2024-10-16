
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class ExcerptConfiguration : IEntityTypeConfiguration<Excerpt>
{
    public void Configure(EntityTypeBuilder<Excerpt> builder)
    {

        builder.Property(p => p.AdditionalInfo)
            .HasMaxLength(2000);
    }
}
