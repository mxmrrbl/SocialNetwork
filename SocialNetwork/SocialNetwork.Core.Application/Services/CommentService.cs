using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class CommentService : GenericService<CommentViewModel, SaveCommentViewModel, Comment>, ICommentService
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly UserViewModel UVM;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IHttpContextAccessor httpcontextAccessor) : base(commentRepository, mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _httpcontextAccessor = httpcontextAccessor;
            UVM = _httpcontextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public override async Task<SaveCommentViewModel> Add(SaveCommentViewModel svm)
        {
            svm.UserId = UVM.Id;
            svm.PhotoUrl = UVM.PhotoUrl;
            Comment comment= _mapper.Map<Comment>(svm);

            comment = await _commentRepository.AddAsync(comment);

            SaveCommentViewModel Svm = _mapper.Map<SaveCommentViewModel>(comment);

            return Svm;
        }

        public override async Task<List<CommentViewModel>> GetAllViewModel()
        {
            var commentList = await _commentRepository.GetAllAsyncWithIncludes();

            return commentList.Select(comment => new CommentViewModel
            {
                Id = comment.Id,
                UserId = comment.UserId,
                PostId = comment.PostId,
                Content = comment.Content,
                UserName = comment.User.UserName,
                PhotoUrl = comment.PhotoUrl,

            }).ToList();
            
            
        }
    }

}
