
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class ExcerptCommentConfiguration : IEntityTypeConfiguration<ExcerptComment>
{
    public void Configure(EntityTypeBuilder<ExcerptComment> builder)
    {

        builder.Property(p => p.Details)
            .HasMaxLength(2000);
    }
}
