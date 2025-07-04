﻿
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Permissions.Commands.CreatePermission;

public class DeletePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
       private readonly IApplicationDbContext _context;

    public DeletePermissionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync(BeUnique)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
        RuleFor(v => v.NameEN)
           .MaximumLength(100)
           .NotEmpty();
        RuleFor(v => v.NameAR)
           .MaximumLength(100)
           .NotEmpty();
        RuleFor(v => v.Description)
            .MaximumLength(200)
            .NotEmpty();

    }

    public async Task<bool> BeUnique(string name, CancellationToken cancellationToken)
    {
        return await _context.ApplicationPermissions
            .AllAsync(l => l.PolicyName != name, cancellationToken);
    }

}
