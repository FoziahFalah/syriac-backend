using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles;
public class ApplicationRoleDto
{
    public int Id { get; set; }
    public string? NormalizedRoleName { get; set; }
    public string? NameEN { get; set; }
    public string? NameAR { get; set; }
    public string? Name => $"{NameEN} - {NameAR}"; //=> read-only

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationRole, ApplicationRoleDto>()
                .ForMember(x=>x.Name, opt => opt.Ignore());
        }
    }
}
