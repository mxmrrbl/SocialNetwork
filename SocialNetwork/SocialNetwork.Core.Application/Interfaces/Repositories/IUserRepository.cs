using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel lvm);
    }
}
