using ThFnsc.NFe.Core.Entities.Shared;

namespace ThFnsc.NFe.Data.Repositories
{
    public static class EntityRepository
    {
        public static IQueryable<T> OfId<T>(this IQueryable<T> input, int id) where T : BaseEntity =>
            input.Where(e => e.Id == id);
    }
}
