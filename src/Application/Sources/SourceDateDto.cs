using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Sources
{
    public class SourceDateDto
    {
        public int DateFormatId { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public string? Format { get; set; }
        public string? Period { get; set; }
    }
}
