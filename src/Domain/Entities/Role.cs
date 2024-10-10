using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Infrastructure.Identity;
public class Role :BaseAuditableEntity
{
    public string? Name_ar { get; set; }
    public string? Description { get; set; }
    
}
