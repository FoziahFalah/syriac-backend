﻿using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Common.Models;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Roles.Commands.CreateRole;
[Authorize(Policy = "roles:create-role")]
public record CreateRoleCommand : IRequest<Result>
{
    public required string NameEN { get; init; }
    public required string NameAR { get; init; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRoleService _appRoleService;

    public CreateRoleCommandHandler(IApplicationDbContext context, IApplicationRoleService appRoleService)
    {
        _context = context;
        _appRoleService = appRoleService;
    }

    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new ApplicationRole
        {
            NormalizedRoleName = request.NameEN.Normalize(),
            NameEN = request.NameEN,
            NameAR = request.NameAR,
        };

        var result = await _appRoleService.CreateAsync(entity, cancellationToken);

        return result;
    }
}
