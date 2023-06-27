using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string? PhotoUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsValidated { get; set; }

        //Nav. Props

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Friendship> Users { get; set; }
        public ICollection<Friendship> Friends { get; set; }


    }
}
