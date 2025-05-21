using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Sources
{
    public class SourceDateDto
    {
        public int DateFormatId { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public string? Format { get; set; }
        public string? Period { get; set; }


        public class Mapping : Profile
        {
            public Mapping()
            {

                CreateMap<SourceDate, SourceDateDto>()
            .ForMember(dest => dest.Format, opt => opt.MapFrom(src => src.DateFormat != null ? src.DateFormat.Format : null))
            .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.DateFormat != null ? src.DateFormat.Period : null))
            .ReverseMap();


              
            }
        }
    }
}
