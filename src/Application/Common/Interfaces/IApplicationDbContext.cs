using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Attachment> Attachments { get; }
    DbSet<Author> Authors { get; }
    DbSet<Century> Centuries { get; }
    DbSet<Comment> Comments { get; }
    DbSet<Contributor> Contributors { get; }
    DbSet<CoverPhoto> CoverPhotos { get; }
    DbSet<DateFormat> DateFromats { get; }
    DbSet<Excerpt> Excerpts { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<ExcerptDate> ExcerptDates { get; }
    DbSet<ExcerptText> ExcerptTexts { get; }
    DbSet<Footnote> Footnotes { get; }
    DbSet<Language> Languages { get; }
    DbSet<SourcePublication> Publications { get; }
    DbSet<SourceInroductionEditor> SourceInroductionEditors { get; }
    DbSet<Source> Sources { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
