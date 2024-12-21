using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Application.Features.Training.Services
{
    public class TagManagementService : ITagManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public TagManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<(IList<Tag> records, int total, int totalDisplay)>
            GetPagedTagsAsync(int pageIndex, int pageSize, string searchText, string sortBy)
        {
            return await _unitOfWork.TagRepository.GetTableDataAsync(searchText, sortBy, pageIndex, pageSize);
        }
    }
}
