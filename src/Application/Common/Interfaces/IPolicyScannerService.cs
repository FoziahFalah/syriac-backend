using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Common.Interfaces
{
    public interface IPolicyScannerService
    {
        HashSet<string> DiscoverPolicies();
    }

}
