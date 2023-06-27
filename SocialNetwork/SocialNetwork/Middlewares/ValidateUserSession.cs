using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Application.Helpers;

namespace SocialNetwork.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;

        public  ValidateUserSession(IHttpContextAccessor httpcontextAccessor)
        {
            _httpcontextAccessor = httpcontextAccessor;
        }

        public bool HasUser(){
            UserViewModel uvm = _httpcontextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            if (uvm == null) 
            { 
                return false;
            }
           
            return true;
        }
    }
}
