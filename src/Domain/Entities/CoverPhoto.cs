﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Domain.Entities;
public class CoverPhoto : BaseAuditableEntity
{
    public int SourceId { get; set; }
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
    public string? FileExtension { get; set; }

}
