namespace SyriacSources.Backend.Application.TodoItems.Queries.GetAccessPermissions;

public class GetAccessPermissionsQueryValidator : AbstractValidator<GetAccessPermissionsQuery>
{
    public GetAccessPermissionsQueryValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required.");

    }
}
