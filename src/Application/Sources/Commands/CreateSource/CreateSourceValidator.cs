using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Constants;

namespace SyriacSources.Backend.Application.Sources.Commands.CreateSource
{
    public class CreateSourceValidator : AbstractValidator<CreateSource>
    {
        public CreateSourceValidator()
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
