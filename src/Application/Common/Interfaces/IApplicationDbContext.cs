using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    public DbSet<TodoList> TodoLists{ get; }

    public DbSet<TodoItem> TodoItems { get; }
    public DbSet<Attachment> Attachments { get; }
    public DbSet<Author> Authors { get; }
    public DbSet<Century> Centuries { get; }
    public DbSet<ExcerptComment> Comments { get; }
    public DbSet<Contributor> Contributors { get; }
    public DbSet<CoverPhoto> CoverPhotos { get; }
    public DbSet<ApplicationRolePermission> ApplicationRolePermissions { get; }
    public DbSet<ApplicationPermission> ApplicationPermissions { get; }
    public DbSet<ApplicationRole> ApplicationRoles { get; }
    public DbSet<ApplicationUserRole> ApplicationUserRoles { get; }
    public DbSet<DateFormat> DateFromats { get; }
    public DbSet<Excerpt> Excerpts { get; }
    public DbSet<ExcerptDate> ExcerptDates { get; }
    public DbSet<ExcerptText> ExcerptTexts { get; }
    public DbSet<Footnote> Footnotes { get; }
    public DbSet<Language> Languages{ get; }
    public DbSet<Publication> Publications { get; }
    public DbSet<SourceIntroEditor> SourceIntroductionEditors { get; }
    public DbSet<Source> Sources { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
