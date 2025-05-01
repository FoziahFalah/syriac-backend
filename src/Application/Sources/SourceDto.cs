using AutoMapper;
using SyriacSources.Backend.Application.Sources.Commands.CreateSource;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Sources;
public class SourceDto
{
    public int Id { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string CenturyName { get; set; } = string.Empty;
    public DateTime DocumentedOnHijri { get; set; }
    public DateTime DocumentedOnGregorian { get; set; }
    public string? Introduction { get; set; }
    public string? SourceTitleInArabic { get; set; }
    public string? SourceTitleInSyriac { get; set; }
    public string? SourceTitleInForeignLanguage { get; set; }
    public string? IntroductionEditorName { get; set; }
    public string? AdditionalInfo { get; set; }
    public List<PublicationDto> Publications { get; set; } = new();
    public List<AttachmentDto> OtherAttachments { get; set; } = new();
    public CoverPhotoDto? CoverPhoto { get; set; }
    // الخصائص من الكلاس الأب:
    public DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Source, SourceDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.CenturyName, opt => opt.MapFrom(src => src.Century.Name))
                .ForMember(dest => dest.IntroductionEditorName,
                    opt => opt.MapFrom(src => src.IntroductionEditor != null ? src.IntroductionEditor.FullNameAR : null));
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<Publication, PublicationDto>();
            CreateMap<CoverPhoto, CoverPhotoDto>();
        }
    }
}



