using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.TodoItems.Queries.GetAccessPermissions;

public class AccessPermissionsDto
{
    public int Id { get; init; }
    public string? DisplayName { get; init; }
    public List<EndpointActions>? Actions { get; init; }

    private class Mapping : Profile
    {

    }
}
