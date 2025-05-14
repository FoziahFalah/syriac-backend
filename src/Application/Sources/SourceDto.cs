using AutoMapper;
using SyriacSources.Backend.Application.Sources.Commands.CreateSource;
using SyriacSources.Backend.Domain.Entities;
namespace SyriacSources.Backend.Application.Sources;
public class SourceDto
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public int CenturyId { get; set; }
    public string CenturyName { get; set; } = string.Empty;
    public int? IntroductionEditorId { get; set; }
    public string? IntroductionEditorName { get; set; }
    public string? Introduction { get; set; }
    public string? SourceTitleInArabic { get; set; }
    public string? SourceTitleInSyriac { get; set; }
    public string? SourceTitleInForeignLanguage { get; set; }
    public string? AdditionalInfo { get; set; }
    public List<PublicationDto> Publications { get; set; } = new();
    public List<AttachmentDto> OtherAttachments { get; set; } = new();
    public CoverPhotoDto? CoverPhoto { get; set; }
    public List<SourceDateDto> SourceDates { get; set; } = new();
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
                .ForMember(dest => dest.IntroductionEditorName, opt => opt.MapFrom(
                    src => src.IntroductionEditor != null ? src.IntroductionEditor.FullNameAR : null))
                .ForMember(dest => dest.SourceDates, opt => opt.MapFrom(src => src.SourceDates));
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<Publication, PublicationDto>();
            CreateMap<CoverPhoto, CoverPhotoDto>();
            CreateMap<SourceDate, SourceDateDto>()
                .ForMember(dest => dest.Format, opt => opt.MapFrom(src => src.DateFormat != null ? src.DateFormat.Format : null))
                .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.DateFormat != null ? src.DateFormat.Period : null));
        }
    }
}























