using System.Reflection;
using SyriacSources.Backend.Application.Common.Exceptions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.Common.Security;

namespace SyriacSources.Backend.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUser _user;
    private readonly IIdentityApplicationUserService _identityService;
    private readonly IApplicationUserRoleService _appUserRoleService;
    private readonly PolicyManagementOptions _policyManagementOptions;

    public AuthorizationBehaviour(
        IUser user,
        IApplicationUserRoleService appUserRoleService,
        PolicyManagementOptions policyManagementOptions,
        IIdentityApplicationUserService identityService)
    {
        _user = user;
        _identityService = identityService;
        _policyManagementOptions = policyManagementOptions;
        _appUserRoleService = appUserRoleService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_user.Id == null)
            {
                throw new UnauthorizedAccessException();
            }


            // Roles-based Admin authorization
            bool authorized = false;

            string authorizedAllAttributes = _policyManagementOptions.AutoPolicyAllowedRoleNamesCsv; // || String.IsNullOrEmpty(authorizedAllAttributes
            if (!String.IsNullOrEmpty(authorizedAllAttributes))
            {
                //Check if it's a super authenticated user
                foreach (var role in authorizedAllAttributes.Split(';'))
                {
                    bool isInRole = await _appUserRoleService.IsInRoleAsync(Convert.ToInt32(_user.Id), Convert.ToInt32(role), cancellationToken);
                    if (isInRole)
                    {
                        authorized = true;
                        break;
                    }
                }
            }


            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            if (authorizeAttributesWithRoles.Any())
            {
                authorized = false;

                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    foreach (var role in roles)
                    {
                        bool isInRole = await _appUserRoleService.IsInRoleAsync(Convert.ToInt32(_user.Id), Convert.ToInt32(role),cancellationToken);
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }

            // Policy-based authorization
            var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                {
                    authorized = await _identityService.AuthorizeAsync(_user.Id, policy);

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}
