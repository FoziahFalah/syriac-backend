using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Models;

namespace SyriacSources.Backend.Application.Common.Extensions;
public static class ContextResultExtension
{
    public static Result ToApplicationResult(this int result)
    {
        return result > 0
            ? Result.Success(null)
            : Result.Failure(new List<String>() { "Error occured"});
    }
}
