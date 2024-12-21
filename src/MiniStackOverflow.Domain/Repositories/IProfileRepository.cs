using MiniStackOverflow.Domain.Entities;

namespace MiniStackOverflow.Domain.Repositories
{
    public interface IProfileRepository : IRepositoryBase<User, Guid>
    {
        Task<bool> IsUserNameDuplicateAsync(string userName, Guid? id = null);
        Task<User> GetByUserNameAsync(string userName);
    }
}
