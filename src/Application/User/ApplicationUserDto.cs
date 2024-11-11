using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Application.Common.Interfaces;
using SyriacSources.Backend.Application.Permissions.Queries;
using SyriacSources.Backend.Application.TodoLists.Queries.GetTodos;
using SyriacSources.Backend.Domain.Entities;

namespace SyriacSources.Backend.Application.User;
public class ApplicationUserDto
{
    public int Id { get; set; }
    public string? NameEN { get; set; }
    public string? NameAR { get; set; }
    public string? EmailAddress { get; set; }
}
