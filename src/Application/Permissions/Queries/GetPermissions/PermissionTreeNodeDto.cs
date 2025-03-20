using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Permissions.Queries.GetPermissions
{
    public class PermissionTreeNodeDto
    {
        public required string Key { get; set; }
        public bool? Checked { get; set; }
        public ApplicationPermissionDto Data { get; set; } = null!;
        public List<PermissionTreeNodeDto>? Children { get; set; } // For nested permissions
        public string? Label => $"{Data.NameEN} - {Data.NameAR}";
    }
}
