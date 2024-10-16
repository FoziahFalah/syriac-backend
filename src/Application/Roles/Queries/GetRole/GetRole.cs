using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;

namespace SyriacSources.Backend.Application.Roles.Queries.GetRole;

public record GetRoleCommand : IRequest<string>
{
    public int Id { get; set; }
}
public class GetRoleHandler : IRequestHandler<GetRoleCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _identityRoleService;

    public GetRoleHandler(IApplicationDbContext context, IApplicationRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task<string> Handle(GetRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _identityRoleService.GetRoleAsync(request.Id.ToString());

        Guard.Against.NotFound(request.Id, entity);

        //string result = await _identityRoleService.GetRoleAsync(request.Id.ToString());

        return entity;
    }
}
