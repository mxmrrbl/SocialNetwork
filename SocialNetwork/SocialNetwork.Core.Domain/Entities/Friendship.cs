using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Friendship
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FriendId{ get; set; }

        //Nav. Props

        public User User { get; set; }
        public User Friend { get; set; }
    }
}
