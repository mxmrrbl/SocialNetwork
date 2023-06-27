using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class SavePostViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PostPhotoUrl { get; set; }
        public string Content { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
