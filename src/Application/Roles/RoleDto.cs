using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;
using SyriacSources.Backend.Infrastructure.Identity;

namespace SyriacSources.Backend.Application.Roles;
public class RoleDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }

    private class Mapping : Profile { 
    
        public Mapping(){
            CreateMap<Role, RoleDto>();
        }
    }  
}
