
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class ExcerptDateConfiguration : IEntityTypeConfiguration<ExcerptDate>
{
    public void Configure(EntityTypeBuilder<ExcerptDate> builder)
    {

    }
}
