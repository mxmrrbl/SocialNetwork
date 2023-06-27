using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Friendship;
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
    public class FriendshipService : GenericService<FriendshipViewModel, SaveFriendshipViewModel, Friendship>, IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly IMapper _mapper;
        private readonly UserViewModel UVM;

        public FriendshipService(IFriendshipRepository friendshipRepository, IMapper mapper, IHttpContextAccessor httpcontextAccessor) : base(friendshipRepository, mapper)
        {
            _friendshipRepository = friendshipRepository;
            _mapper = mapper;
            _httpcontextAccessor = httpcontextAccessor;
            UVM = _httpcontextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task AddBidirectionalFriendship(SaveUserViewModel suvm)
        {

            Friendship friendship = new();
            friendship.UserId = UVM.Id;
            friendship.FriendId = suvm.Id;

            await _friendshipRepository.AddAsync(friendship);

            Friendship friendship2 = new();
            friendship2.UserId = suvm.Id;
            friendship2.FriendId = UVM.Id;

            await _friendshipRepository.AddAsync(friendship2);

        }

        public async Task<List<FriendshipViewModel>> GetAllMyFriendships()
        {
            var FriendshipList = await _friendshipRepository.GetAllAsyncWithIncludes();

            return FriendshipList.Where(friendship => friendship.UserId == UVM.Id).Select(friendship => new FriendshipViewModel
            {
                Id = friendship.Id,
                UserId = friendship.UserId,
                FriendId = friendship.FriendId,
                PhotoUrl = friendship.Friend.PhotoUrl,
                UserName = friendship.Friend.UserName,

            }).ToList();
            
        }

        public async Task<SaveFriendshipViewModel> VeryfyFriendshipExist(SaveUserViewModel suvm)
        {
            SaveFriendshipViewModel sfvm = new();
            sfvm.UserId = UVM.Id;
            sfvm.FriendId = suvm.Id;

            List<Friendship> friendshipList = await _friendshipRepository.GetAllAsync();

            Friendship friendship = friendshipList.FirstOrDefault(f => f.UserId == sfvm.UserId && f.FriendId == sfvm.FriendId);

            SaveFriendshipViewModel friendshipExist = _mapper.Map<SaveFriendshipViewModel>(friendship);

            return friendshipExist;
        }

        public List<PostViewModel> FilterFriendsPosts(List<FriendshipViewModel> flist, List<PostViewModel> postList)
        {
            List<PostViewModel> myFrendsPostsList = new();

            List<FriendshipViewModel> FrendshipList = flist;

            List<PostViewModel> allPostsList = postList;

            foreach (FriendshipViewModel friend in FrendshipList)
            {
                foreach (PostViewModel post in allPostsList)
                {
                    if (post.UserId == friend.FriendId)
                    {
                        myFrendsPostsList.Add(post);
                    }
                }
            }

            return myFrendsPostsList;
        }

        public override async Task Delete(int id)
        {
            var friendship = await _friendshipRepository.GetByIdAsync(id);

            var allFriendships = await _friendshipRepository.GetAllAsync();

            var friendship2 = allFriendships.FirstOrDefault(f => f.UserId == friendship.FriendId && f.FriendId == friendship.UserId);

            await _friendshipRepository.DeleteAsync(friendship);

            await _friendshipRepository.DeleteAsync(friendship2);
        }
    }
}
