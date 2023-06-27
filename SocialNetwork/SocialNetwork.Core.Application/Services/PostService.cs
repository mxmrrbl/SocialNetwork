using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class PostService : GenericService<PostViewModel, SavePostViewModel, Post>, IPostService
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly UserViewModel UVM;

        public PostService(IPostRepository postRepository, IMapper mapper, IHttpContextAccessor httpcontextAccessor) : base(postRepository, mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _httpcontextAccessor = httpcontextAccessor;
            UVM = _httpcontextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public override async Task<SavePostViewModel> Add(SavePostViewModel svm)
        {
            svm.UserId = UVM.Id;
            svm.PhotoUrl = UVM.PhotoUrl;
            Post post = _mapper.Map<Post>(svm);

            post = await _postRepository.AddAsync(post);

            SavePostViewModel Svm = _mapper.Map<SavePostViewModel>(post);
            Svm.File = svm.File;

            return Svm;
        }

        public override async Task<List<PostViewModel>> GetAllViewModel()
        {
            var postList = await _postRepository.GetAllAsync();

            return postList.Select(post => new PostViewModel
            {
                Id = post.Id,
                PhotoUrl = post.PhotoUrl,
                PostPhotoUrl = post.PostPhotoUrl,
                UserName = post.User.UserName,
                UserId = post.UserId,
                Content = post.Content,
                CreationDate = post.CreationDate

            }).ToList();
        }

    }
}
