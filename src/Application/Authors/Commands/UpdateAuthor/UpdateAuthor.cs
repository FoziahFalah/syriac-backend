using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SyriacSources.Backend.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
namespace SyriacSources.Backend.Application.Authors.Commands.UpdateAuthor;
public record UpdateAuthorCommand(int Id, string Name) : IRequest;
public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("اسم المؤلف مطلوب.");
    }
}
public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateAuthorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (author == null)
            throw new KeyNotFoundException($"المؤلف بالمعرف {request.Id} غير موجود.");
        author.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
