using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface ITokenService
{
    Task<string> CreateJwtSecurityToken(string id, ApplicationUserRole role);
}

