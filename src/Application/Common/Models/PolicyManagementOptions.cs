﻿
namespace SyriacSources.Backend.Application.Common.Models;
public  class PolicyManagementOptions
{
    public PolicyManagementOptions()
    {
        PolicyNamesToConfigureAsAllowAnonymous = new List<string>();
        PolicyNamesToConfigureAsAnyAuthenticatedUser = new List<string>();
    }

    public bool AutoCreateMissingPolicies { get; set; } = true;
    public string AutoPolicyAllowedRoleNamesCsv { get; set; } = "Administrator";

    public bool ShowRequireAuthenticatedUserOption { get; set; } = true;

    public bool ShowRequiredUserNameOption { get; set; } = true;
    public bool ShowAuthenticationSchemeOptions { get; set; } = true;
    public bool ShowClaimRequirementOptions { get; set; }

    public List<string> PolicyNamesToConfigureAsAllowAnonymous { get; set; }

    public List<string> PolicyNamesToConfigureAsAnyAuthenticatedUser { get; set; }
}
