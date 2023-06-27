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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        
        private readonly ApplicationContext _dbContext;

        public PostRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task<List<Post>> GetAllAsync()
        {
            return await _dbContext.Posts
               .Include(post => post.User).ToListAsync();
        }


    }
}
