using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles;
public class ApplicationRoleDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Name_ar { get; set; }
    public string? Description { get; set; }

    private class Mapping : Profile { 
    
        public Mapping(){} // CreateMap<ApplicationRole, ApplicationRoleDto>();

    }
}
