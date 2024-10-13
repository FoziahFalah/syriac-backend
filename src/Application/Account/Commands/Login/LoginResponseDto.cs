using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.User;

namespace SyriacSources.Backend.Application.Account.Commands.Login;
public class LoginResponseDto
{
    public ApplicationUserDto? User { get; set; }
    public string? Token { get; set; }
    public bool Succeeded { get; set; } = true;
    public IEnumerable<string>? Errors { get; set; }

}

