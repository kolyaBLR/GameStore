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
    [Table("game")]
    public class Game
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Column("game_id")]
        public int GameId { get; set; }

        [Display(Name = "Название")]
        [MaxLength(64, ErrorMessage = "Слишком длинное название")]
        [Column("name")]
        [Required(ErrorMessage = "Пожалуйста, введите название для игры")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [MaxLength(1024, ErrorMessage = "Слишком длинное описание")]
        [Column("description")]
        [Required(ErrorMessage = "Пожалуйста, введите описание для игры")]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Категория")]
        [MaxLength(64, ErrorMessage = "Слишком длинное название категории")]
        [Column("category")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию для игры")]
        //public Category Category { get; set; }

        [Display(Name = "Цена (руб)")]
        [Required]
        [Column("price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }

        [Column("Image_data")]
        public byte[] ImageData { get; set; }

        [Column("Image_mime_type")]
        public string ImageMimeType { get; set; }
    }
}