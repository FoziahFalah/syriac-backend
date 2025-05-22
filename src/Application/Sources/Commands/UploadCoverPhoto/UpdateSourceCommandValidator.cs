using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Constants;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Sources.Commands.UpdateSource
{
    public class UploadCoverPhotoCommandValidator : AbstractValidator<UploadCoverPhotoCommand>
    {
        public UploadCoverPhotoCommandValidator()
        {
            RuleFor(v => v.File)
            .NotEmpty();
        }
    }
}
