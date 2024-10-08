using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Domain.Entities;
public class Permission : BaseAuditableEntity
{
    public string? PermissionName { get; set; }
    public string? Description { get; set; }
}
