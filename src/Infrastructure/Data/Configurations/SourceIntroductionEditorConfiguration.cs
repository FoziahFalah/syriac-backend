
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Infrastructure.Data.Configurations;
public class SourceIntroductionEditorConfiiguration : IEntityTypeConfiguration<SourceIntroEditor>
{
    public void Configure(EntityTypeBuilder<SourceIntroEditor> builder)
    {

    }
}
