using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.Common.Models;
public class JWTToken
{
    public required string Secret {  get; set; }
    public required string ValidIssuer {  get; set; }
    public required string ValidAudience {  get; set; }
    public required int ExpiryByHours { get; set; }
}
