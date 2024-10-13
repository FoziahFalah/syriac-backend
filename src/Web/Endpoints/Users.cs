using SyriacSources.Backend.Application.User;

namespace SyriacSources.Backend.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUserDto>();
    }
}
