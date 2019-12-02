using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace your_Blog.Models
{
    /// <summary>
    /// Категория.
    /// </summary>
    public class CategoryModel
    {
        [HiddenInput]
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название категории")]
        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; set; }


        public ICollection<ArticleModel> Articles { get; set; }

        public CategoryModel()
        {
            Articles = new List<ArticleModel>();
        }
    }
}