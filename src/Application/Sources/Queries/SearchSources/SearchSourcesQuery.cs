using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Sources.Queries.SearchSources
{
    public class SearchSourcesQuery : IRequest<List<SourceDto>>
    {
        public string? SourceTitleInArabic { get; set; }
        public string? SourceTitleInSyriac { get; set; }
        public string? SourceTitleInForeignLanguage { get; set; }
        public string? Introduction { get; set; }
        public string? AuthorName { get; set; }
        public string? IntroductionEditorName { get; set; }
        public string? CenturyName { get; set; }
        public string? DocumentedOn { get; set; }
    }
    public class SearchSourcesQueryHandler : IRequestHandler<SearchSourcesQuery, List<SourceDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SearchSourcesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<SourceDto>> Handle(SearchSourcesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Sources
                .Include(x => x.Author)
                .Include(x => x.Century)
                .Include(x => x.IntroductionEditor)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SourceTitleInArabic))
                query = query.Where(x => x.SourceTitleInArabic != null && x.SourceTitleInArabic.Contains(request.SourceTitleInArabic));
            if (!string.IsNullOrWhiteSpace(request.SourceTitleInSyriac))
                query = query.Where(x => x.SourceTitleInSyriac != null && x.SourceTitleInSyriac.Contains(request.SourceTitleInSyriac));
            if (!string.IsNullOrWhiteSpace(request.SourceTitleInForeignLanguage))
                query = query.Where(x => x.SourceTitleInForeignLanguage != null && x.SourceTitleInForeignLanguage.Contains(request.SourceTitleInForeignLanguage));
            if (!string.IsNullOrWhiteSpace(request.Introduction))
                query = query.Where(x => x.Introduction != null && x.Introduction.Contains(request.Introduction));
            if (!string.IsNullOrWhiteSpace(request.AuthorName))
                query = query.Where(x => x.Author.Name.Contains(request.AuthorName));
            if (!string.IsNullOrWhiteSpace(request.CenturyName))
                query = query.Where(x => x.Century.Name.Contains(request.CenturyName));
            if (!string.IsNullOrWhiteSpace(request.IntroductionEditorName))
            {
                query = query.Where(x =>
                    (x.IntroductionEditor.FullNameAR != null && x.IntroductionEditor.FullNameAR.Contains(request.IntroductionEditorName)) ||
                    (x.IntroductionEditor.FullNameEN != null && x.IntroductionEditor.FullNameEN.Contains(request.IntroductionEditorName)));
            }
            if (!string.IsNullOrWhiteSpace(request.DocumentedOn))
            {
                query = query.Where(x =>
                    x.DocumentedOnHijri.ToString().Contains(request.DocumentedOn) ||
                    x.DocumentedOnGregorian.ToString().Contains(request.DocumentedOn));
            }
            var result = await query.ToListAsync(cancellationToken);
            return _mapper.Map<List<SourceDto>>(result);
        }
    }
}
