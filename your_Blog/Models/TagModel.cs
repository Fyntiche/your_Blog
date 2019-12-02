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
        [HiddenInput]
        /// <summary>
        /// Идентификатор тега статьи.
        /// </summary>
        public int Id { get; set; }

        [Required]
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