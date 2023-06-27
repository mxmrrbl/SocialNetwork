using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            await base.AddAsync(entity);
            return entity;
        }

        public async Task<User> LoginAsync(LoginViewModel lvm)
        {
            string passwordEncrypt = PasswordEncryption.ComputeSha256Hash(lvm.Password);
            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.UserName == lvm.UserName && user.Password == passwordEncrypt);
            return user;
        }

    }
}
