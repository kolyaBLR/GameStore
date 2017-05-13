using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameStore.Domain.Entities
{
    [Table("user")]
    public class User
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Пожалуйста, введите ваше имя")]
        [MaxLength(128, ErrorMessage = "Слишком длинное имя")]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Пожалуйста, введите вашу фамилию")]
        [MaxLength(128, ErrorMessage = "Слишком длинное имя")]
        [Column("last_name")]
        public string LastName { get; set; }

        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [MaxLength(128)]
        [Column("email")]
        [Index(IsUnique = true)]
        [EmailAddress(ErrorMessage = "Введите корректный email адрес")]
        [Required(ErrorMessage = "Email не должен быть пустым")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [MaxLength(64, ErrorMessage = "Слишком длинный пароль")]
        [MinLength(8, ErrorMessage = "Слишком короткий пароль")]
        [Required(ErrorMessage = "Пароль не должен быть пустым")]
        [Column("passwod")]
        public string Passwod { get; set; }
    }
}
