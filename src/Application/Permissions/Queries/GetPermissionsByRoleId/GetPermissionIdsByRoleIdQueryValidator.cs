namespace SyriacSources.Backend.Application.Permissions.Queries.GetPermissionsByRoleId;

public class GetPermissionIdsByRoleIdQueryValidator : AbstractValidator<GetPermissionsByRoleIdQuery>
{
    public GetPermissionIdsByRoleIdQueryValidator()
    {
        RuleFor(x => x.roleId)
            .NotEmpty().WithMessage("RoleId is required.");
    }
}
