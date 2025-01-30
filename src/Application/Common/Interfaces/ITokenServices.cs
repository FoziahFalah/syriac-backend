using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.User;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Common.Interfaces;

public interface ITokenService
{
    Task<string> CreateJwtSecurityToken(UserBasicDetailsVm login);
}

