using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Sources
{
    public class AttachmentDto
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? FileExtension { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
