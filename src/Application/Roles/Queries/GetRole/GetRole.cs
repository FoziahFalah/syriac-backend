using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles.Queries.GetRole;

public record GetRoleCommand : IRequest<ApplicationRole>
{
    public int Id { get; set; }
}
public class GetRoleHandler : IRequestHandler<GetRoleCommand, ApplicationRole>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _identityRoleService;

    public GetRoleHandler(IApplicationDbContext context, IApplicationRoleService identityRoleService)
    {
        _context = context;
        _identityRoleService = identityRoleService;
    }

    public async Task<ApplicationRole> Handle(GetRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _identityRoleService.GetRoleAsync(request.Id,cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        return entity;
    }
}
