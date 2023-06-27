using AutoMapper;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Friendship;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            //User

            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Friends, opt => opt.Ignore())
                    .ReverseMap();
                
            CreateMap<SaveUserViewModel, User>()
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Friends, opt => opt.Ignore())
                    .ReverseMap()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore());


            //Post

            CreateMap<PostViewModel, Post>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                    .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.Ignore());

            CreateMap<SavePostViewModel, Post>()
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.CreationDate, opt => opt.Ignore())
                .ReverseMap();
                

            //Comment

            CreateMap<CommentViewModel, Comment>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                    .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.Ignore());

            CreateMap<SaveCommentViewModel, Comment>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                    .ReverseMap();

            //Friendship

            CreateMap<FriendshipViewModel, Friendship>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Friend, opt => opt.Ignore())
                    .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore());


            CreateMap<SaveFriendshipViewModel, Friendship>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Friend, opt => opt.Ignore())
                    .ReverseMap();

        }

    }
}
