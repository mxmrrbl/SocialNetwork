using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class NewPassViewModel
    {
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe repetir la contraseña")]
        public string ConfirmPassword { get; set; }

        public int UserId { get; set; }
}
}
