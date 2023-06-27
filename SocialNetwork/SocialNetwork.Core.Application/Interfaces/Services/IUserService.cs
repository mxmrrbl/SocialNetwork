using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<UserViewModel, SaveUserViewModel, User>
    {
        Task<UserViewModel> Login(LoginViewModel lvm);
        Task<SaveUserViewModel> VeryfyUserExist(string userName);

        Task UpdatePassword(SaveUserViewModel svm, int id);
    }
}
