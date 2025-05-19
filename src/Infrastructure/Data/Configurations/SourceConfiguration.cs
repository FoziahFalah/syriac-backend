
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class SourceConfiguration : IEntityTypeConfiguration<Source>
{
    public void Configure(EntityTypeBuilder<Source> builder)
    {

        builder.Property(p => p.Introduction)
           .HasMaxLength(2000)
           .IsRequired();

        builder.Property(p => p.SourceTitleInArabic)
           .HasMaxLength(500)
           .IsRequired();

        builder.Property(p => p.SourceTitleInSyriac)
           .HasMaxLength(500)
           .IsRequired();

        builder.Property(p => p.SourceTitleInForeignLanguage)
           .HasMaxLength(500)
           .IsRequired();

        builder.Property(p => p.AdditionalInfo)
            .HasMaxLength(2000);

        ////Indexes
        builder.HasIndex(p => p.SourceTitleInArabic)
            .HasDatabaseName("SourceTitleInArabic");

        builder.HasOne(s => s.CoverPhoto)
          .WithOne()
          .HasForeignKey<CoverPhoto>(c => c.SourceId)
          .OnDelete(DeleteBehavior.Restrict);
    }
}
