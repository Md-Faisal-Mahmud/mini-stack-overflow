using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Application.Features.Training.Services
{
    public interface ITagManagementService
    {
        Task<(IList<Tag> records, int total, int totalDisplay)>
           GetPagedTagsAsync(int pageIndex, int pageSize, string searchText, string sortBy);
    }
}