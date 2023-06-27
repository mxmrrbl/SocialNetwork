using AutoMapper;
using SocialNetwork.Core.Application.DTOs.Email;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class UserService : GenericService<UserViewModel, SaveUserViewModel, User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<SaveUserViewModel> VeryfyUserExist(string userName)
        {
            List<User> userList = await _userRepository.GetAllAsync();

            User user = userList.FirstOrDefault(u => u.UserName == userName);

            SaveUserViewModel userExist = _mapper.Map<SaveUserViewModel>(user);

            return userExist;
        }

        public async Task<UserViewModel> Login(LoginViewModel lvm)
        {
            User user = await _userRepository.LoginAsync(lvm);

            if (user == null)
            {
                return null;
            }

            UserViewModel uvm = _mapper.Map<UserViewModel>(user);

            return uvm;
        }

        public override async Task<SaveUserViewModel> Add(SaveUserViewModel svm)
        {
            User user = _mapper.Map<User>(svm);


            user = await _userRepository.AddAsync(user);

            SaveUserViewModel Svm = _mapper.Map<SaveUserViewModel>(user);

            await _emailService.SendAsync(new EmailRequest
            {
                To = user.Email,
                Subject = $"Welcome to Social Network",
                Body = $"<h1>Welcome to Social Network {user.Name}</h1> <p>Your username is {user.UserName}, to validate your user  <a href='https://localhost:7250/Users/VerifyUser/{user.Id}'> click here </a> </p>"
            });

            return Svm;
        }

        public async Task UpdatePassword(SaveUserViewModel svm, int id)
        {
            User user = _mapper.Map<User>(svm);

            user.Password = PasswordEncryption.ComputeSha256Hash(user.Password);
            await _userRepository.UpdateAsync(user, id);

            await _emailService.SendAsync(new EmailRequest
            {
                To = user.Email,
                Subject = $"Password Changed",
                Body = $"<h1>Your password has been changed to: {svm.Password}</p>"
            });
        }

    }
}
