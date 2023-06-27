using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PostPhotoUrl { get; set; }
        public string Content { get; set; }

        public DateTime? CreationDate { get; set; } = DateTime.Now;

        //Nav. Props

        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
