
using Microsoft.EntityFrameworkCore;
using MiniStackOverflow.Domain.Entities;
using MiniStackOverflow.Domain.Repositories;
using MiniStackOverflow.Infrastructure;
using System.Linq.Expressions;

namespace MiniStackOverflow.Infrastructure.Repositories
{
    public class TagRepository : Repository<Tag, Guid>, ITagRepository
    {
        public TagRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<(IList<Tag> records, int total, int totalDisplay)> GetTableDataAsync(string searchText, string orderBy, int pageIndex, int pageSize)
        {
            Expression<Func<Tag, bool>> expression = null;
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                expression = x => x.TagName.Contains(searchText);
            }
            return await GetDynamicAsync(expression, orderBy, null, pageIndex, pageSize, true);
        }
    }
}
