using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Domain.Entities;
public class Contributor :BaseAuditableEntity
{
    public string? Name { get; set; }
    public int UserId { get; set; }
}
