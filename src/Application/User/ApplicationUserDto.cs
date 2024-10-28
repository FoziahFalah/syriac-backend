using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;

namespace SyriacSources.Backend.Application.User;
public class ApplicationUserDto
{
    public int Id { get; set; }
    public string? NameEN { get; set; }
    public string? NameAR { get; set; }
    public string? Email { get; set; }
}
