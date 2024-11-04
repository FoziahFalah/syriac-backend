using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Common.Models;
public class ClaimRequirement
{
    public ClaimRequirement()
    {
        AllowedValues = new List<string>();
    }

    public string ClaimName { get; set; }
    public List<string> AllowedValues { get; set; }

}
