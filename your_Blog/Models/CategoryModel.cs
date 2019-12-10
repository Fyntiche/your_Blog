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
        [Key]
        [HiddenInput]
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int Id { get; set; }

        [Required(ErrorMessage = "Название категории должно быть заполнено")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
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