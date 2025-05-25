using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Sources
{
    public class UploadedFileDto
    {
        public int? Id { get; set; } // جديد
        public string FileName { get; set; } = default!;
        public string FilePath { get; set; } = default!;
        public string FileExtension { get; set; } = default!;
    }
}
