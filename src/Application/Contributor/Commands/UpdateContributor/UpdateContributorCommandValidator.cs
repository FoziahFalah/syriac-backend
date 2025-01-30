
using SyriacSources.Backend.Application.Common.Extensions;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Contributors.Commands.UpdateContributor;

namespace SyriacSources.Backend.Application.Contributors.Commands.UpdateContributor;

public class UpdateContributorCommandValidator : AbstractValidator<UpdateContributorCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateContributorCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.NameAR)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(v => v.NameEN)
            .MaximumLength(100);

    }

}
