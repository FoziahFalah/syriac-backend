using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Domain.Entities;
public class Permissions : BaseAuditableEntity
{
    public int RoleId {  get; set; }
    public int Policy { get; set; }
}
