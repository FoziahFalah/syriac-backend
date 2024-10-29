using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class ApplicationPermissionConfiguration : IEntityTypeConfiguration<ApplicationPermission>
{
    public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
    {

        builder.Property(p => p.NameAR)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.NameEN)
           .HasMaxLength(200)
           .IsRequired();

        builder.Property(p => p.PolicyName)
           .HasMaxLength(200)
           .IsRequired();

        //Indexes
        builder.HasIndex(p => p.PolicyName)
            .IsUnique()
            .HasDatabaseName("PolicyName");
    }
}
