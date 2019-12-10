using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace your_Blog.Models
{
    /// <summary>
    /// Статья.
    /// </summary>
    public class ArticleModel
    {
        [Key]
        [HiddenInput]
        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public int Id { get; set; }

        [Required(ErrorMessage = "Название статьи должно быть заполнено")]
        [Display(Name = "Название статьи")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 200 символов")]
        /// <summary>
        /// Название статьи.
        /// </summary>
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Краткое описание статьи")]
        /// <summary>
        /// Краткое описание статьи.
        /// </summary>
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Текст статьи должно быть заполнено")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Текст статьи")]
        /// <summary>
        /// Текст статьи.
        /// </summary>
        public string Description { get; set; }

        [HiddenInput]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Дата создания")]
        /// <summary>
        /// Дата содания статьи.
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// Связь с категорией статьи.
        /// </summary>
        public int CategoryId { get; set; }
        [Display(Name = "Категория статьи")]
        /// <summary>
        /// Навигационное своейство категории статьи.
        /// </summary>
        public virtual CategoryModel Category { get; set; }

        [Display(Name = "Теги")]
        public virtual ICollection<TagModel> Tags { get; set; }

        public ArticleModel()
        {
            Tags = new List<TagModel>();
        }

        [Display(Name = "Фотография")]
        public byte[] HeroImage { get; set; }

    }
}