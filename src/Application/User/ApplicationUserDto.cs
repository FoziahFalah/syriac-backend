using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyriacSources.Backend.Application.User;
public class ApplicationUserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Name_ar { get; set; }
    public string? Username { get; set; }
    public string? Fullname { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? DeactivatedBy { get; set; }
    public DateTime Deactivatedon { get; set; }
}
