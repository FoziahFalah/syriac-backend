using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Domain.Entities;
using Attachment = SyriacSources.Backend.Domain.Entities.Attachment;

namespace SyriacSources.Backend.Application.Sources
{
    public class AttachmentDto
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? FileExtension { get; set; }
        public DateTimeOffset Created { get; set; }

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Attachment, AttachmentDto>().ReverseMap();
            }
        }
    }
}
    
