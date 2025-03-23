
using SyriacSources.Backend.Application.Roles;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Permissions.Queries.GetPermissions
{
    public class ApplicationPermissionDto
    { 
        public int Id { get; set; }
        public bool IsModule { get; set; }
        public required string NameEN { get; set; }
        public required string NameAR { get; set; }
        public string? Description { get; set; }
        public int ParentId { get; set; }
        public string? Label => $"{NameEN} - {NameAR}";


        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<ApplicationPermission, ApplicationPermissionDto>()
                    .ForMember(x => x.Label, opt => opt.Ignore());
            }
        }
    }
}
