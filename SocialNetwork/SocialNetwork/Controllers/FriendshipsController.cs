using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Friendship;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Middlewares;

namespace SocialNetwork.Controllers
{
    public class FriendshipsController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly IFriendshipService _friendshipService;
        private readonly UserViewModel UVM;

        public FriendshipsController(ValidateUserSession validateUserSession, IPostService postService, ICommentService commentService, IUserService userService, IFriendshipService friendshipService, IHttpContextAccessor httpcontextAccessor)
        {
            _validateUserSession = validateUserSession;
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
            _friendshipService = friendshipService;
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

            ViewBag.fr = await _friendshipService.GetAllMyFriendships();

            return View(_friendshipService.FilterFriendsPosts(await _friendshipService.GetAllMyFriendships(), await _postService.GetAllViewModel()));

        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(SaveUserViewModel suvm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            ViewBag.ms = "";

            var userExist = await _userService.VeryfyUserExist(suvm.UserName);

            //Verify if isn't me and if isn't my friend already
            if (userExist != null && userExist.Id != UVM.Id)
            {
                var friendExist = await _friendshipService.VeryfyFriendshipExist(userExist);

                if(friendExist == null)
                {
                    await _friendshipService.AddBidirectionalFriendship(userExist);

                    return RedirectToRoute(new { controller = "Friendships", action = "Index" });
                }
                else
                {
                    ViewBag.ms = "Ya son amigos";
                }  
            }
            else if(userExist.Id == UVM.Id)
            {
                ViewBag.ms = "No puedes agregarte a ti mismo";
            }
            else
            {
                ViewBag.ms = "El usuario no existe";
            }

            //Return view with error messages
            ViewBag.cm = await _commentService.GetAllViewModel();

            ViewBag.fr = await _friendshipService.GetAllMyFriendships();

            return View("Index", _friendshipService.FilterFriendsPosts(await _friendshipService.GetAllMyFriendships(), await _postService.GetAllViewModel()));
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View("DeletePost", await _friendshipService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _friendshipService.Delete(id);

            return RedirectToRoute(new { controller = "Friendships", action = "Index" });
        }

        
    }
}
