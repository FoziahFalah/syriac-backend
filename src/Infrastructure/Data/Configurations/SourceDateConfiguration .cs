using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations
{
    public class SourceDateConfiguration : IEntityTypeConfiguration<SourceDate>
    {
        public void Configure(EntityTypeBuilder<SourceDate> builder)
        {
            builder.ToTable("SourceDates");
            builder.HasKey(sd => sd.Id);
            builder.HasOne(sd => sd.Source)
                   .WithMany(s => s.SourceDates)
                   .HasForeignKey(sd => sd.SourceId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(sd => sd.DateFormat)
                   .WithMany()
                   .HasForeignKey(sd => sd.DateFormatId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.Property(sd => sd.FromYear).IsRequired();
            builder.Property(sd => sd.ToYear).IsRequired();
        }
    }
}
