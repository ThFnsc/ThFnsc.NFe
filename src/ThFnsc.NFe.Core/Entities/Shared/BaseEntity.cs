using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ThFnsc.NFe.Core.Entities.Shared
{
    [Index(nameof(CreatedAt))]
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public BaseEntity()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
