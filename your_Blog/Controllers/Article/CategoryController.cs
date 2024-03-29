﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using your_Blog.Models;

namespace your_Blog.Controllers.Article
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList().Where(c => c.Id != 1));
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryModel categoryModel = db.Categories.Find(id);

            if (categoryModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryModel);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categoryModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryModel);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryModel categoryModel = db.Categories.Find(id);

            if (categoryModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryModel);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryModel);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel categoryModel = db.Categories.Find(id);
            if (categoryModel == null)
            {
                return HttpNotFound();
            }
            return View(categoryModel);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryModel categoryModel = db.Categories.Find(id);
            var article = db.Articles.Where(p => p.CategoryId == id).ToList();

            foreach (var item in article)
            {
                ArticleModel articleModel = db.Articles.Find(item.Id);
                articleModel.CategoryId = 1;
                db.Entry(articleModel).State = EntityState.Modified;
                db.SaveChanges();
            }

            db.Categories.Remove(categoryModel);
            db.SaveChanges();

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
    }
}
