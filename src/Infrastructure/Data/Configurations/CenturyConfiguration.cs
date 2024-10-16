
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class CenturyConfiguration : IEntityTypeConfiguration<Century>
{
    public void Configure(EntityTypeBuilder<Century> builder)
    {

        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();


        //Indexes
        builder.HasIndex(p => p.Name)
            .HasDatabaseName("CenturyName");
    }
}
