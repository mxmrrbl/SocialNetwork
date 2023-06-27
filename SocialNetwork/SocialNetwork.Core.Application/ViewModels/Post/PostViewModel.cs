using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? PhotoUrl { get; set; }
        public string Content { get; set; }
        public string? PostPhotoUrl { get; set; }
        public DateTime? CreationDate { get; set; }

        public string UserName { get; set; }
    }
}
