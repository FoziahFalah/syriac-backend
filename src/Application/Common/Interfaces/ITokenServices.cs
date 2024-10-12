using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface ITokenService
{
    string CreateJwtSecurityToken(string? id);
}

