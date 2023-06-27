using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Middlewares;

namespace SocialNetwork.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        private readonly ValidateUserSession _validateUserSession;

        public UsersController(IUserService userService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel lvm)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(lvm);
            }

            UserViewModel uvm = await _userService.Login(lvm);

            if(uvm != null && uvm.IsValidated == true)
            {
                HttpContext.Session.Set<UserViewModel>("user", uvm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            }else if(uvm != null && uvm.IsValidated == false)
            {
                ModelState.AddModelError("userValidation", "Revise su correo, el user no está verificado");
            }
            else
            {
                ModelState.AddModelError("userValidation", "Credenciales incorrectas");
            }

            return View(lvm);
        }

        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new SaveUserViewModel());
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Users", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel suvm)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(suvm);
            }

            SaveUserViewModel userExist = await _userService.VeryfyUserExist(suvm.UserName);

            if(userExist == null)
            {
                SaveUserViewModel userVM = await _userService.Add(suvm);
                if (userVM != null && userVM.Id != 0)
                {
                    userVM.PhotoUrl = UploadFile(suvm.File, userVM.Id);
                    await _userService.Update(userVM, userVM.Id);
                }

                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "El usuario ya existe");
            }

            return View(suvm);

        }

        public IActionResult ValidateUserResetPass()
        {
            return View(new ValidateUserViewModel());
        }

        public async Task<IActionResult> NewPassword(ValidateUserViewModel vm)
        {

            SaveUserViewModel userExist = await _userService.VeryfyUserExist(vm.UserName);

            if(userExist == null)
            {
                ViewBag.ms = "El user no existe";
                return View("ValidateUserResetPass", vm);
            }

            NewPassViewModel newPassViewModel = new();
            newPassViewModel.UserId = userExist.Id;

            return View(newPassViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> SetNewPassword(NewPassViewModel npvm)
        {
            SaveUserViewModel userVM = await _userService.GetByIdSaveViewModel(npvm.UserId);
            userVM.Password = npvm.Password;

            if (npvm.Password != npvm.ConfirmPassword)
            {
                ViewBag.ms = "Las Contraseñas no coinciden";
                return View("NewPassword", npvm);
            }

            await _userService.UpdatePassword(userVM, userVM.Id);

            return RedirectToRoute(new { controller = "Users", action = "Index" });
        }

        public async Task<IActionResult> VerifyUser(int id)
        {
            var user = await _userService.GetByIdSaveViewModel(id);
            user.IsValidated = true;

            await _userService.Update(user, user.Id);

            return View("Index");
        }


        private string UploadFile(IFormFile file, int id)
        {
            //get directory path
            string basePath = $"/Images/Users/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if doesn't exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file path
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string FileNameWithRoute = Path.Combine(path, fileName);

            using (var stream = new FileStream(FileNameWithRoute, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"{basePath}/{fileName}";
        }
    }
}
