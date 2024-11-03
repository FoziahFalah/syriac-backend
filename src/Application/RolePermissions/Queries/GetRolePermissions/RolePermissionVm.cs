
using SyriacSources.Backend.Application.Permissions.Queries;
using SyriacSources.Backend.Application.Roles;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.RolePermissions.Queries.GetRolePermissions;

public class RolePermissionVm
{
    public int ApplicationRoleId { get; set; }
    public List<int>? ApplicationPermissionIds { get; set; } 

    private class Mapping : Profile 
    {
        public Mapping()
        {
           
        }
    }
}
