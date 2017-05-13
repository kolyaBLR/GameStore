using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models.Authentication
{
    public class LoginModel
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [MaxLength(128, ErrorMessage = "Слишком длинный email")]
        [EmailAddress(ErrorMessage = "Введите корректный email адрес")]
        [Required(ErrorMessage = "Email не должен быть пустым")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [MaxLength(64, ErrorMessage = "Слишком длинный пароль")]
        [MinLength(8, ErrorMessage = "Слишком короткий пароль")]
        [Required(ErrorMessage = "Пароль не должен быть пустым")]
        public string Password { get; set; }
    }
}