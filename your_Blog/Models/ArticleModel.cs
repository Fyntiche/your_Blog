using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace your_Blog.Models
{
    /// <summary>
    /// Статья.
    /// </summary>
    public class ArticleModel
    {
        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название статьи.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Краткое описание статьи.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Дата содания статьи.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Связь с категорией статьи.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Навигационное своейство категории статьи.
        /// </summary>
        public CategoryModel Category { get; set; }

        public virtual ICollection<TagModel> Tags { get; set; }

        public ArticleModel()
        {
            Tags = new List<TagModel>();
        }
        

        // :TODO изображение public byte[] Hero_Image { get; set; }



    }
}