using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.Sources
{
    public class PublicationDto
    {
        public string? Description { get; set; }
        public string Url { get; set; } = string.Empty;


        public class Mapping : Profile
        {
            public Mapping()
            {

                CreateMap<Publication, PublicationDto>().ReverseMap();
            }
        }
    }
}
