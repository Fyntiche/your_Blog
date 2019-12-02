using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace your_Blog.Models
{
    /// <summary>
    /// Статья.
    /// </summary>
    public class ArticleModel
    {
        [HiddenInput]
        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название статьи")]
        /// <summary>
        /// Название статьи.
        /// </summary>
        public string Name { get; set; }

        [Display(Name = "Краткое описание статьи")]
        /// <summary>
        /// Краткое описание статьи.
        /// </summary>
        public string ShortDescription { get; set; }

        [Display(Name = "Дата создания")]
        /// <summary>
        /// Дата содания статьи.
        /// </summary>
        public DateTime Date { get; set; }

        [Display(Name = "Категория статьи")]
        /// <summary>
        /// Связь с категорией статьи.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Навигационное своейство категории статьи.
        /// </summary>
        public CategoryModel Category { get; set; }

        [Display(Name = "Теги")]
        public virtual ICollection<TagModel> Tags { get; set; }

        public ArticleModel()
        {
            Tags = new List<TagModel>();
        }
        

        // :TODO изображение public byte[] Hero_Image { get; set; }



    }
}