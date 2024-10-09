using System.Reflection;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SyriacSources.Backend.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Century> Centuries => Set<Century>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Contributor> Contributors => Set<Contributor>();
    public DbSet<CoverPhoto> CoverPhotos => Set<CoverPhoto>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<DateFormat> DateFromats => Set<DateFormat>();
    public DbSet<Excerpt> Excerpts => Set<Excerpt>();
    public DbSet<ExcerptDate> ExcerptDates => Set<ExcerptDate>();
    public DbSet<ExcerptText> ExcerptTexts => Set<ExcerptText>();
    public DbSet<Footnote> Footnotes => Set<Footnote>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<SourcePublication> Publications => Set<SourcePublication>();
    public DbSet<SourceInroductionEditor> SourceInroductionEditors => Set<SourceInroductionEditor>();
    public DbSet<Source> Sources => Set<Source>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
