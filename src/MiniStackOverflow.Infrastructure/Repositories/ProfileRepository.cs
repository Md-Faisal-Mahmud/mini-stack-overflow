﻿
using Microsoft.EntityFrameworkCore;
using MiniStackOverflow.Domain.Entities;
using MiniStackOverflow.Domain.Repositories;
using MiniStackOverflow.Infrastructure;
using System.Linq.Expressions;

namespace MiniStackOverflow.Infrastructure.Repositories
{
    public class ProfileRepository : Repository<User, Guid>, IProfileRepository
    {
        public ProfileRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            Expression<Func<User, bool>> filter = q => q.UserName == userName;
            return (await GetAsync(filter, null)).FirstOrDefault();
        }

        public async Task<bool> IsUserNameDuplicateAsync(string userName, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await GetCountAsync(x => x.Id != id.Value && x.UserName == userName) > 0;
            }
            else
            {
                return await GetCountAsync(x => x.UserName == userName) > 0;
            }
        }
    }
}
