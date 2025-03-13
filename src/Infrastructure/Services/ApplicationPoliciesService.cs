
using MediatR;
using System.Reflection;

namespace SyriacSources.Backend.Infrastructure.Services;
public class ApplicationPoliciesService
{
    private readonly IApplicationDbContext _context; 
    private readonly IApplicationPermissionService _permissionService;

    public ApplicationPoliciesService(IApplicationDbContext context, IApplicationPermissionService permissionService)
    {
        _context = context;
        _permissionService = permissionService;
    }


    /// <summary>
    /// Scans all MediatR request classes for [Authorize(Policy = "...")] and registers them in the database.
    /// </summary>
    public async Task RegisterApplicationPoliciesAsync()
    {
        var policies = new HashSet<string>();

        // Get all types implementing IRequest<> or IRequest
        var requestTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>) ||
                i == typeof(IRequest)))
            .ToList();

        foreach (var type in requestTypes)
        {
            var authorizeAttributes = type.GetCustomAttributes<AuthorizeAttribute>().ToList();
            foreach (var attr in authorizeAttributes)
            {
                if (!string.IsNullOrEmpty(attr.Policy))
                {
                    policies.Add(attr.Policy);
                }
            }
        }

        // Insert policies into the database if they do not exist
        foreach (var policy in policies)
        {
            if (!await _context.ApplicationPermissions.AnyAsync(p => p.PolicyName == policy))
            {
                await _permissionService.CreatePolicy(policy, new CancellationToken());
            }
        }
    }

    //public async Task RegisterApplicationPoliciesAsync(WebApplication app)
    //{
    //    var policies = new HashSet<string>();

    //    // Get all endpoints
    //    var endpoints = app.Services.GetRequiredService<EndpointDataSource>().Endpoints;

    //    foreach (var endpoint in endpoints)
    //    {
    //        // Check for metadata that includes the Authorize attribute
    //        var authorizeMetadata = endpoint.Metadata.GetMetadata<IAuthorizeData>();
    //        if (authorizeMetadata != null && !string.IsNullOrEmpty(authorizeMetadata.Policy))
    //        {
    //            policies.Add(authorizeMetadata.Policy);
    //        }
    //    }
        
    //    // Insert policies into the database if they do not exist
    //    foreach (var policy in policies)
        
    //    if (!await _context.ApplicationPermissions.AnyAsync(p => p.PolicyName == policy))
    //    {
    //        await _permissionService.CreatePolicy(policy, new CancellationToken());
    //    }
    //}

    
}
