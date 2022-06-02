using System.Linq;
using ThFnsc.NFe.Core.Entities.Shared;

namespace ThFnsc.NFe.Data.Repositories;

public static class SoftDeleteEntityRepository
{
    public static IQueryable<T> Active<T>(this IQueryable<T> input) where T : BaseSoftDeleteEntity =>
        input.Where(e => e.DeletedAt == null);
}
