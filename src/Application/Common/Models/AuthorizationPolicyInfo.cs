
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Common.Models;
public class AuthorizationPolicyInfo
{
    public AuthorizationPolicyInfo()
    {
        Id = Guid.NewGuid();
        AllowedRoles = new List<string>();
        AuthenticationSchemes = new List<string>();
        RequiredClaims = new List<ClaimRequirement>();
    }
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool RequireAuthenticatedUser { get; set; }
    public string? RequiredUserName { get; set; }
    public List<string> AllowedRoles { get; set; }
    public List<string> AuthenticationSchemes { get; set; }
    public List<ClaimRequirement> RequiredClaims { get; set; }

}
