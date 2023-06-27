using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Middlewares;
using SocialNetwork.Models;
using System.Diagnostics;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly UserViewModel UVM;

        public HomeController(ValidateUserSession validateUserSession, IPostService postService, ICommentService commentService, IHttpContextAccessor httpcontextAccessor)
        {
            _validateUserSession = validateUserSession;
            _postService = postService;
            _commentService = commentService;
            _httpcontextAccessor = httpcontextAccessor;
            UVM = _httpcontextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            ViewBag.cm = await _commentService.GetAllViewModel();

            return View(FilterMyPosts(await _postService.GetAllViewModel()));
        }

        public List<PostViewModel> FilterMyPosts(List<PostViewModel> postList)
        {
            List<PostViewModel> myFrendsPostsList = new();

            List<PostViewModel> allPostsList = postList;

            foreach (PostViewModel post in allPostsList)
            {
                if (post.UserId == UVM.Id)
                {
                    myFrendsPostsList.Add(post);
                }
            }
            
            return myFrendsPostsList;
        }

    }
}