using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace your_Blog.Models
{
    /// <summary>
    /// Тег статьи.
    /// </summary>
    public class TagModel
    {
        [Key]
        [HiddenInput]
        /// <summary>
        /// Идентификатор тега статьи.
        /// </summary>
        public int Id { get; set; }

        [Required(ErrorMessage = "Название тега должно быть заполнено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Название тега")]
        /// <summary>
        /// Название тега статьи.
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<ArticleModel> Articles { get; set; }

        public TagModel()
        {
            Articles = new List<ArticleModel>();
        }
    }
}