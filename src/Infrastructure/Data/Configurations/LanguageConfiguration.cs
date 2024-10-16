
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {

        builder.Property(p => p.Code)
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(p => p.Name)
           .HasMaxLength(7)
           .IsRequired();

        ////Indexes
        //builder.HasIndex(p => p.EmailAddress)
        //    .IsUnique()
        //    .HasDatabaseName("LanguageEmailAddress");
    }
}
