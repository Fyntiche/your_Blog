﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using your_Blog.Models;

namespace your_Blog.Controllers.Article
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Article
        public async Task<ActionResult> Index(int page = 1)
        {
            int pageSize = 3;
            ViewBag.Category = db.Categories.ToList().Where(c => c.Id != 1);
            ViewBag.Tag = db.Tags.ToList();

            IEnumerable<ArticleModel> articles = await db.Articles
                .Include(a => a.Category)
                .OrderByDescending(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Articles.Count() };

            IndexViewModel<ArticleModel> ivm = new IndexViewModel<ArticleModel>() { Articles = articles, pageInfo = pageInfo };

            return View(ivm);
        }


        // GET: Article/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleModel articleModel = await db.Articles.FindAsync(id);


            if (articleModel == null)
            {
                return HttpNotFound();
            }
            return View(articleModel);
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList().Where(c => c.Id != 1).ToList(), "Id", "Name");
            ViewBag.Tags = db.Tags.ToList();

            return View();
        }

        // POST: Article/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "Id,Name,ShortDescription,Description,CategoryId")] ArticleModel articleModel,
            int[] selectedTags,
            HttpPostedFileBase uploadFoto)
        {
            if (ModelState.IsValid)
            {
                articleModel.Date = DateTime.Now;
                if (selectedTags != null)
                {
                    //получаем выбранные теги
                    foreach (var c in db.Tags.Where(co => selectedTags.Contains(co.Id)))
                    {
                        articleModel.Tags.Add(c);
                    }
                }

                if (uploadFoto != null)
                {
                    byte[] fotoData = null;
                    using (var binaryReader = new BinaryReader(uploadFoto.InputStream))
                    {
                        fotoData = binaryReader.ReadBytes(uploadFoto.ContentLength);
                    }
                    articleModel.HeroImage = fotoData;
                }

                db.Articles.Add(articleModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories.ToList().Where(c => c.Id != 1).ToList(), "Id", "Name");

            return View(articleModel);
        }

        // GET: Article/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleModel articleModel = await db.Articles.FindAsync(id);

            if (articleModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.Tags = db.Tags.Include(x => x.Articles).ToList();
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", articleModel.CategoryId);

            return View(articleModel);
        }

        // POST: Article/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            ArticleModel articleModel,
            int[] selectedTags,
            HttpPostedFileBase uploadFoto)
        {
            ArticleModel newAtricle = db.Articles.Find(articleModel.Id);
            newAtricle.Name = articleModel.Name;
            newAtricle.ShortDescription = articleModel.ShortDescription;
            newAtricle.CategoryId = articleModel.CategoryId;
            newAtricle.Description = articleModel.Description;
            newAtricle.Date = DateTime.Now;
            newAtricle.HeroImage = articleModel.HeroImage;

            if (ModelState.IsValid)
            {

                if (selectedTags != null)
                {
                    newAtricle.Tags.Clear();
                    //получаем выбранные теги
                    foreach (var c in db.Tags.Where(co => selectedTags.Contains(co.Id)))
                    {
                        newAtricle.Tags.Add(c);
                    }
                }

                if (uploadFoto != null)
                {
                    byte[] fotoData = null;
                    using (var binaryReader = new BinaryReader(uploadFoto.InputStream))
                    {
                        fotoData = binaryReader.ReadBytes(uploadFoto.ContentLength);
                    }
                    newAtricle.HeroImage = fotoData;
                }

                db.Entry(newAtricle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", articleModel.CategoryId);

            return View(articleModel);
        }

        // GET: Article/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleModel articleModel = await db.Articles.FindAsync(id);

            if (articleModel == null)
            {
                return HttpNotFound();
            }

            return View(articleModel);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ArticleModel articleModel = await db.Articles.FindAsync(id);
            db.Articles.Remove(articleModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Фильтрация статей по категориям.
        /// </summary>
        /// <param name="name">Выбранная категория.</param>
        /// <param name="page">Страница.</param>
        /// <returns>Статьи.</returns>
        public ActionResult Category(string name, int page = 1)
        {
            CategoryModel category = db.Categories.Include(p => p.Articles).FirstOrDefault(t => t.Name == name.ToString());
            if (category == null)
            {
                return RedirectToAction("Index");
            }

            int pageSize = 3;

            IEnumerable<ArticleModel> articles = category.Articles.OrderByDescending(a => a.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = category.Articles.Count() };
            IndexViewModel<ArticleModel> ivm = new IndexViewModel<ArticleModel>() { Articles = articles, pageInfo = pageInfo };

            ViewBag.Title = name;

            ViewBag.Category = db.Categories.ToList().Where(c => c.Id != 1);
            ViewBag.Tag = db.Tags.ToList();

            return View("Index", ivm);
        }

        /// <summary>
        /// Фильтрация статей по тегам.
        /// </summary>
        /// <param name="selectedTags">Выбранные теги.</param>
        /// <param name="page">Страница.</param>
        /// <returns>Статьи.</returns>
        public ActionResult Tag(int[] selectedTags, int page = 1)
        {
            IEnumerable<ArticleModel> articles = default;
            int pageSize = 3;

            PageInfo pageInfo;

            if (selectedTags == null)
            {
                return RedirectToAction("Index");
            }

            if (selectedTags != null && selectedTags.Count() != 1)
            {
                IEnumerable<TagModel> tags = db.Tags.Include(p => p.Articles).Where(co => selectedTags.Contains(co.Id));
                if (tags == null)
                {
                    return RedirectToAction("Index");
                }

                IEnumerable<ArticleModel> articlesbuffer = default;

                foreach (var item in tags)
                {
                    articlesbuffer = item.Articles.OrderByDescending(a => a.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    articles = articlesbuffer.Union(item.Articles.OrderBy(a => a.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList());
                }
                pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = articles.Count() };
            }
            else
            {
                var tagName = selectedTags[0].ToString();
                TagModel tag = db.Tags.Include(p => p.Articles).FirstOrDefault(t => t.Id.ToString() == tagName);
                articles = tag.Articles.OrderByDescending(a => a.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = tag.Articles.Count() };
            }

            IndexViewModel<ArticleModel> ivm = new IndexViewModel<ArticleModel>() { Articles = articles, pageInfo = pageInfo };

            ViewBag.Category = db.Categories.ToList().Where(c => c.Id != 1);
            ViewBag.Tag = db.Tags.ToList();
            ViewBag.tagList = db.Tags.Where(x => selectedTags.Contains(x.Id)).ToList();

            return View("Index", ivm);
        }

        /// <summary>
        /// Фильтрация статей по датам публикации.
        /// </summary>
        /// <param name="dateAt">Дата публикации с.</param>
        /// <param name="dateTo">Дата публикации по. </param>
        /// <param name="page">Страница.</param>
        /// <returns>Статьи.</returns>
        public ActionResult PublicationDate(DateTime dateAt, DateTime dateTo, int page = 1)
        {

            List<ArticleModel> articleModels = db.Articles.ToList();

            if (dateTo < dateAt)
            {
                dateTo = dateAt;
            }

            articleModels = articleModels.Where(p => p.Date >= dateAt && p.Date <= dateTo).ToList();

            if (articleModels == null)
            {
                return RedirectToAction("Index");
            }

            int pageSize = 3;
            IEnumerable<ArticleModel> articles = articleModels.OrderByDescending(a => a.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = articleModels.Count() };
            IndexViewModel<ArticleModel> ivm = new IndexViewModel<ArticleModel>() { Articles = articles, pageInfo = pageInfo };

            ViewBag.Category = db.Categories.ToList().Where(c => c.Id != 1);
            ViewBag.DateAt = dateAt;
            ViewBag.DateTo = dateTo;
            ViewBag.Tag = db.Tags.ToList();

            return View("Index", ivm);
        }

            
    }
}
