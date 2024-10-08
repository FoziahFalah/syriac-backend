using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Domain.Entities;
public class RolePermission : BaseAuditableEntity
{
    public Guid RoleId { get; set; }
    public string? PermissionId { get; set; }
}
