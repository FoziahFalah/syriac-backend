
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {

        builder.Property(p => p.FileName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.FilePath)
           .HasMaxLength(500)
           .IsRequired();

        builder.Property(p => p.FileExtension)
           .HasMaxLength(200)
           .IsRequired();

        ////Indexes
        //builder.HasIndex(p => p.NormalizedPermissionName)
        //    .IsUnique()
        //    .HasDatabaseName("NormalizedPermissionName");
    }
}
