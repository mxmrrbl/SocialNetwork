using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Middlewares;

namespace SocialNetwork.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ValidateUserSession _validateUserSession;

        public CommentsController(ICommentService commentService, ValidateUserSession validateUserSession)
        {
            _commentService = commentService;
            _validateUserSession = validateUserSession;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(SaveCommentViewModel svm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            await _commentService.Add(svm);

            return RedirectToRoute(new { controller = "Friendships", action = "Index" });
        }

        public IActionResult Index()
        {
            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }
    }
}
