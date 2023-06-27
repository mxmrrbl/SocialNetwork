using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class FriendshipRepository : GenericRepository<Friendship>, IFriendshipRepository
    {
        private readonly ApplicationContext _dbContext;

        public FriendshipRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Friendship>> GetAllAsyncWithIncludes()
        {
            return await _dbContext.Friendships
                .Include(f => f.Friend).ToListAsync();
        }
    }
}
