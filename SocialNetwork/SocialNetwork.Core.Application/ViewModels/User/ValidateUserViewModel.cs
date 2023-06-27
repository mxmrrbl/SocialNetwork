using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class ValidateUserViewModel
    {
        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        public string UserName { get; set; }
    }
}
