using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Domain.Entities
{
    public class SourceDate : BaseAuditableEntity
    {
        public int SourceId { get; set; }
        public virtual Source Source { get; set; } = null!;
        public int DateFormatId { get; set; }
        public DateFormat? DateFormat { get; set; } 
        public int FromYear { get; set; }
        public int ToYear { get; set; }
    }
}
