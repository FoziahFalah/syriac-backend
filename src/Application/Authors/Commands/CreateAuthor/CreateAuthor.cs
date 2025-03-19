using FluentValidation;
using MediatR;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
namespace SyriacSources.Backend.Application.Authors.Commands.CreateAuthor;
public record CreateAuthorCommand(string Name) : IRequest<int>;
public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("اسم المؤلف مطلوب.");
    }
}
public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateAuthorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            Name = request.Name
        };
        _context.Authors.Add(author);
        await _context.SaveChangesAsync(cancellationToken);
        return author.Id;
    }
}
