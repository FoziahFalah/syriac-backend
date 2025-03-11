using AutoMapper;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.DateFormats;
public class DateFormatDto
{
    public int Id { get; set; }
    public string Format { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<DateFormat, DateFormatDto>();
        }
    }
}
