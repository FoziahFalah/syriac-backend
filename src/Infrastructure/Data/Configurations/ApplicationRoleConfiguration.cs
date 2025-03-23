using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        // indexing
        builder.HasIndex(u=>u.NormalizedRoleName)
            .IsUnique()
            .HasDatabaseName("NormalizedRoleName");


        builder.Property(t => t.NormalizedRoleName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.NameAR)
           .HasMaxLength(100)
           .IsRequired();

        builder.Property(t => t.NameEN)
          .HasMaxLength(100)
          .IsRequired();

    }
}
