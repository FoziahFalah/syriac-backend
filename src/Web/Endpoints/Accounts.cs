
using SyriacSources.Backend.Application.Account.Commands.Login;

namespace SyriacSources.Backend.Web.Endpoints;

public class Accounts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(Login, "Login");
    }

    /// <summary>
    /// Users Login
    /// </summary>
    public async Task<LoginResponseVm> Login(ISender sender, LoginCommand command)
    {
        return await sender.Send(command);
    }
}
