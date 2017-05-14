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
    [Table("category")]
    public class Category
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Display(Name = "Категория:")]
        [Required(ErrorMessage = "Пожалуйста, введите название категории")]
        [MaxLength(64, ErrorMessage = "Слишком длинное название категории")]
        [Column("name")]
        public string Name { get; set; }
    }
}
