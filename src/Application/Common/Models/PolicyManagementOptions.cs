using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Common.Models;
public  class PolicyManagementOptions
{
    public bool AutoCreateMissingPolicies{get;set;}
    public string? AutoPolicyAllowedRoleNamesCsv{get;set;}
    public bool ShowRequireAuthenticatedUserOption{get;set;}
    public bool ShowRequiredUserNameOption{get;set;}
    public bool ShowAuthenticationSchemeOptions{get;set;}
    public bool ShowClaimRequirementOptions{get;set;}
    public List<string>? PolicyNamesToConfigureAsAllowAnonymous { get; set; }
}
