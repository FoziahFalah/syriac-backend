using System.Reflection;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using SyriacSources.Backend.Infrastructure.Data.Configurations;

namespace SyriacSources.Backend.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityApplicationUser, IdentityApplicationRole, int>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Century> Centuries => Set<Century>();
    public DbSet<ExcerptComment> Comments => Set<ExcerptComment>();
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    
    public DbSet<CoverPhoto> CoverPhotos => Set<CoverPhoto>();
    public DbSet<ApplicationRolePermission> ApplicationRolePermissions => Set<ApplicationRolePermission>();
    public DbSet<ApplicationPermission> ApplicationPermissions => Set<ApplicationPermission>();
    public DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
    public DbSet<ApplicationUserRole> ApplicationUserRoles => Set<ApplicationUserRole>();
    public DbSet<DateFormat> DateFromats => Set<DateFormat>();
    public DbSet<Excerpt> Excerpts => Set<Excerpt>();
    public DbSet<ExcerptDate> ExcerptDates => Set<ExcerptDate>();
    public DbSet<ExcerptText> ExcerptTexts => Set<ExcerptText>();
    public DbSet<Footnote> Footnotes => Set<Footnote>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<Publication> Publications => Set<Publication>();
    public DbSet<SourceIntroEditor> SourceIntroductionEditors => Set<SourceIntroEditor>();
    public DbSet<Source> Sources => Set<Source>();
    public DbSet<SourceDate> SourceDates => Set<SourceDate>();

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfiguration(new SourceConfiguration());
        builder.ApplyConfiguration(new SourceDateConfiguration());
        builder.Entity<ExcerptText>()
       .HasOne(et => et.Editor)
       .WithMany(u => u.EditedExcerpts)
       .HasForeignKey(et => et.EditorId)
       .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<ExcerptText>()
            .HasOne(et => et.Reviewer)
            .WithMany(u => u.ReviewedExcerpts)
            .HasForeignKey(et => et.ReviewerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<ExcerptText>()
            .HasOne(et => et.Translator)
            .WithMany(u => u.TranslatedExcerpts)
            .HasForeignKey(et => et.TranslatorId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<ExcerptDate>()
    .HasOne<Excerpt>()                  // يربطه بالأب
    .WithMany(e => e.ExcerptDates)     // من Excerpt إلى ExcerptDates
    .HasForeignKey(ed => ed.ExcerptId)
    .OnDelete(DeleteBehavior.Cascade); // يحذف إذا انحذف الأب
    }
}
