using AutoMapper;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Authors;
public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Author, AuthorDto>();
        }
    }
}
