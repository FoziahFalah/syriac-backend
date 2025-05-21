using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Constants;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Sources.Commands.UpdateSource
{
    public class UpdateSourceCommandValidator : AbstractValidator<UpdateSourceCommand>
    {
        public UpdateSourceCommandValidator()
        {
            RuleFor(v => v.AuthorId)
                .GreaterThan(0).WithMessage("يجب اختيار مؤلف.");
            RuleFor(v => v.CenturyId)
                .GreaterThan(0).WithMessage("يجب اختيار قرن.");
            RuleFor(v => v.SourceTitleInArabic)
                .NotEmpty().WithMessage("العنوان العربي مطلوب.")
                .MaximumLength(200).WithMessage("الحد الأقصى لعدد الأحرف هو 200.");
            RuleFor(v => v.SourceTitleInSyriac)
                .NotEmpty().WithMessage("العنوان بالسريانية مطلوب.")
                .MaximumLength(200).WithMessage("الحد الأقصى لعدد الأحرف هو 200.");
            RuleFor(v => v.SourceTitleInForeignLanguage)
                .NotEmpty().WithMessage("العنوان بلغة أجنبية مطلوب.")
                .MaximumLength(200).WithMessage("الحد الأقصى لعدد الأحرف هو 200.");
        }
    }
}
