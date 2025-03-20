using System.Reflection;
using SyriacSources.Backend.Application.Common.Interfaces;
public class PolicyScannerService : IPolicyScannerService
{
    public HashSet<string> DiscoverPolicies()
    {
        var policies = new HashSet<string>();

        // Load the correct Application Layer Assembly
        var applicationAssembly = Assembly.GetAssembly(typeof(PolicyScannerService)); // Gets the Application layer assembly

        if (applicationAssembly == null)
            throw new Exception("Application Assembly could not be found!");

        var types = applicationAssembly.GetTypes();

        // Scan for [Authorize(Policy = "...")] attributes
        foreach (var type in types)
        {
            var authorizeAttributes = type.GetCustomAttributes<AuthorizeAttribute>();
            foreach (var attr in authorizeAttributes)
            {
                if (!string.IsNullOrEmpty(attr.Policy))
                {
                    policies.Add(attr.Policy);
                }
            }
        }

        return policies;
    }
}
