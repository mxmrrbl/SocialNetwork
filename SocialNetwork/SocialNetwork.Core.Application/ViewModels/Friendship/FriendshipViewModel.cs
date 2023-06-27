using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Friendship
{
    public class FriendshipViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }

        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
