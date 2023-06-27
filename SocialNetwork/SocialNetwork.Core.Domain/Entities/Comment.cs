using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string? PhotoUrl { get; set; }

        //Nav. Props

        public User User { get; set; }
        public Post Post { get; set; }  
    }
}
