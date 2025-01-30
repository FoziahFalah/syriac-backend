using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Domain.Entities;
public class ApplicationUser : BaseAuditableEntity
{
    public string? FullNameEN{ get; set; }
    public string? FullNameAR { get; set; }
    public string? EmailAddress { get; set; }
    public UserType UserType { get; set; }
}
