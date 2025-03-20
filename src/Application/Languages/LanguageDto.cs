using AutoMapper;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Languages;
public class LanguageDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Language, LanguageDto>();
        }
    }
}
