using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string Password { get; set; }
        
    }
}
