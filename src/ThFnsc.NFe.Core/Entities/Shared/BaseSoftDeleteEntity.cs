using Microsoft.EntityFrameworkCore;
using System;

namespace ThFnsc.NFe.Core.Entities.Shared;

[Index(nameof(DeletedAt))]
public abstract class BaseSoftDeleteEntity : BaseEntity
{
    public DateTimeOffset? DeletedAt { get; private set; }

    public BaseSoftDeleteEntity Delete()
    {
        if (DeletedAt.HasValue)
            throw new InvalidOperationException("Entity cannot be deleted twice");
        DeletedAt = DateTimeOffset.UtcNow;
        return this;
    }
}
