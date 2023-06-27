using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Post;

namespace SocialNetwork.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(SavePostViewModel svm)
        {
           
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            SavePostViewModel postVM = await _postService.Add(svm);

            if (postVM.File != null)
            {
                if (postVM != null && postVM.Id != 0)
                {
                    postVM.PostPhotoUrl = UploadFile(svm.File, postVM.Id);
                    await _postService.Update(postVM, postVM.Id);
                }
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id)
        {
            //get directory path
            string basePath = $"/Images/Posts/{id}";
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

        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            return View("DeletePost", await _postService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.Delete(id);

            //get directory path
            string basePath = $"/Images/Posts/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");


            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
