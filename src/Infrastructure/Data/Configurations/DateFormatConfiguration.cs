
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class DateFormatConfiguration : IEntityTypeConfiguration<DateFormat>
{
    public void Configure(EntityTypeBuilder<DateFormat> builder)
    {

        builder.Property(p => p.Period)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Format)
             .HasMaxLength(200)
             .IsRequired();

        //Indexes
        builder.HasIndex(p => p.Format)
            .HasDatabaseName("DateFormatName");

        builder.HasIndex(p => p.Period)
            .HasDatabaseName("DateFormatPeriod");
    }
}
