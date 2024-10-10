
namespace SyriacSources.Backend.Infrastructure.Identity;

public class User :BaseAuditableEntity
{
    public string? FullName { get; set; }
    public int ApplicationUserId { get; set; }
}
