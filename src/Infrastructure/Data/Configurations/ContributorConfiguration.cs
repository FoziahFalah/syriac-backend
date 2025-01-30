
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class ContributorConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

        builder.Property(p => p.FullNameAR)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.FullNameEN)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.EmailAddress)
            .HasMaxLength(200)
            .IsRequired();

        //Indexes
        builder.HasIndex(p => p.FullNameEN)
            .HasDatabaseName("ContributorFullNameEN");

        builder.HasIndex(p => p.FullNameAR)
            .HasDatabaseName("ContributorFullNameAR");

        builder.HasIndex(p => p.EmailAddress)
            .IsUnique()
            .HasDatabaseName("ContributorEmailAddress");
    }
}
