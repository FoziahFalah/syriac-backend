
using MediatR;

namespace SyriacSources.Backend.Domain.Entities;
public class SourceInroductionEditor : BaseAuditableEntity
{ 
    public int SourceId {  get; set; }
    public int EditorId {  get; set; }
    public Contributor Editor {  get; set; } = new Contributor();
}

