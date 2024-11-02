using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.User;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Account.Commands.Login;
public class LoginResponseVm
{
    public UserBasicDetailsVm? UserBasicDetails { get; set; }
    public string? Token { get; set; }
    public bool Succeeded { get; set; } = true;
    public IEnumerable<string>? Errors { get; set; }

}

