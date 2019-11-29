using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace your_Blog.Models
{
    /// <summary>
    /// Категория.
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public int Id { get; set; }

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