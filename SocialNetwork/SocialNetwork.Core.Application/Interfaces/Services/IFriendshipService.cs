using SocialNetwork.Core.Application.ViewModels.Friendship;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IFriendshipService : IGenericService<FriendshipViewModel, SaveFriendshipViewModel, Friendship>
    {
        Task AddBidirectionalFriendship(SaveUserViewModel suvm);
        Task<SaveFriendshipViewModel> VeryfyFriendshipExist(SaveUserViewModel suvm);
        Task<List<FriendshipViewModel>> GetAllMyFriendships();
        List<PostViewModel> FilterFriendsPosts(List<FriendshipViewModel> flist, List<PostViewModel> postList);
    }
}
