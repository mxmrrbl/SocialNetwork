using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Comment
{
    public class SaveCommentViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
