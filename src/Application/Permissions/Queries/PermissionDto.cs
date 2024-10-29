using System.Security;
using SyriacSources.Backend.Application.TodoLists.Queries.GetTodos;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Permissions.Queries;

public class PermissionDto
{
    public int Id { get; init; }
    public int ParentId { get; init; }
    public string? EndpointName { get; init; }
    public required string EndpointGroup { get; init; }
    public string Policy => $"{EndpointGroup}:{EndpointName}";
    public string? DisplayName { get; init; }
    public IEnumerable<PermissionDto>? Actions { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationPermission, PermissionDto>();
        }
    }
}
