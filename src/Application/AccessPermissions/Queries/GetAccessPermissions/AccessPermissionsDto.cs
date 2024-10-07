using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.TodoItems.Queries.GetAccessPermissions;

public class AccessPermissionsDto
{
    public int Id { get; init; }
    public string? DisplayName { get; init; }
    public List<EndpointActions>? Actions { get; init; }

    private class Mapping : Profile
    {
        //public Mapping()
        //{
        //    CreateMap<TodoItem, AccessPermissionsDto>();
        //}
    }
}


public class EndpointActions
{
    public int Id { get; init; }
    public int EndpointId { get; init; }
    public string? DisplayName { get; init; }
}
