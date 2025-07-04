﻿namespace SyriacSources.Backend.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public bool IsActive { get; set; } = true;
    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
