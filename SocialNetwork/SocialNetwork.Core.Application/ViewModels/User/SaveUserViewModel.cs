using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Debe colocar un apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Debe colocar un teléfono")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
        public string? PhotoUrl { get; set; }
        [Required(ErrorMessage = "Debe colocar un email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage = "Las contraseñas no coinciden")]
        [Required(ErrorMessage = "Debe repetir la contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsValidated { get; set; } = false;

        [Required(ErrorMessage = "Debe colocar una foto")]
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
