using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.Sources.Queries.SearchSources
{
    public class SearchSources : IRequest<List<SourceDto>>
    {
        public string? SourceTitleInArabic { get; set; }
        public string? SourceTitleInSyriac { get; set; }
        public string? SourceTitleInForeignLanguage { get; set; }
        public string? Introduction { get; set; }
        public List<int>? AuthorIds { get; set; }
        public List<int>? CenturyIds { get; set; }
        public int? IntroductionEditorId { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public string? Period { get; set; }
    }
    public class SearchSourcesHandler : IRequestHandler<SearchSources, List<SourceDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SearchSourcesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<SourceDto>> Handle(SearchSources request, CancellationToken cancellationToken)
        {
            var query = _context.Sources
                .Include(x => x.SourceDates).ThenInclude(d => d.DateFormat)
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
            if (request.AuthorIds != null && request.AuthorIds.Any())
                query = query.Where(x => request.AuthorIds.Contains(x.AuthorId));
            if (request.CenturyIds != null && request.CenturyIds.Any())
                query = query.Where(x => request.CenturyIds.Contains(x.CenturyId));
            if (request.IntroductionEditorId.HasValue)
                query = query.Where(x => x.IntroductionEditorId == request.IntroductionEditorId.Value);
            if (!string.IsNullOrWhiteSpace(request.Period))
                query = query.Where(x => x.SourceDates.Any(d => d.DateFormat != null && d.DateFormat.Period == request.Period));
            if (request.FromYear.HasValue)
                query = query.Where(x => x.SourceDates.Any(d => d.FromYear >= request.FromYear.Value));
            if (request.ToYear.HasValue)
                query = query.Where(x => x.SourceDates.Any(d => d.ToYear <= request.ToYear.Value));
            var result = await query.ToListAsync(cancellationToken);
            return _mapper.Map<List<SourceDto>>(result);
        }
    }
}






