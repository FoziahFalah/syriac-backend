using System.Security;
using SyriacSources.Backend.Application.TodoLists.Queries.GetTodos;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Permissions.Queries;

public class PermissionDto
{
    public int Id { get; init; }
    public required string PolicyName { get; set; }
    public string? NameEN { get; set; }
    public string? NameAR { get; set; }
    public int ParentId { get; set; } = 0;
    public bool IsModule { get; set; } = false;
    public string? Description { get; set; }
    //public int ParentId { get; init; }
    //public string? EndpointName { get; init; }
    //public required string EndpointGroup { get; init; }
    //public string Policy => $"{EndpointGroup}:{EndpointName}";
    //public string? DisplayName { get; init; }
    //public IEnumerable<ApplicationPermissionDto>? Actions { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationPermission, PermissionDto>();
        }
    }
}
