using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Domain.Repositories
{
    public interface ITagRepository : IRepositoryBase<Tag, Guid>
    {
        Task<(IList<Tag> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchText, string orderBy, int pageIndex, int pageSize);
    }
}
