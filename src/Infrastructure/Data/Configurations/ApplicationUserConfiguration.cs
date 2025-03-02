
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

        builder.Property(p => p.FullNameAR)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.FullNameEN)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(200)
            .IsRequired();

        //Indexes
        builder.HasIndex(p => p.FullNameEN)
            .HasDatabaseName("ApplicationUserFullNameEN");

        builder.HasIndex(p => p.FullNameAR)
            .HasDatabaseName("ApplicationUserFullNameAR");

        builder.HasIndex(p => p.Email)
            .IsUnique()
            .HasDatabaseName("ApplicationUserEmailAddress");
    }
}
