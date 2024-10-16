using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Domain.Entities;
public class CoverPhoto : BaseAuditableEntity
{
    public int SourceId { get; set; }
    public required string FileName { get; set; }
    public required string FilePath { get; set; }
    public required string FileExtension { get; set; }

}
