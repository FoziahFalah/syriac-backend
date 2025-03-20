using AutoMapper;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Centuries;
public class CenturyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Century, CenturyDto>();
        }
    }
}
