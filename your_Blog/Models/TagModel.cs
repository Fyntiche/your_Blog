using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace your_Blog.Models
{
    /// <summary>
    /// Тег статьи.
    /// </summary>
    public class TagModel
    {
        /// <summary>
        /// Идентификатор тега статьи.
        /// </summary>
        public int Id { get; set; }

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