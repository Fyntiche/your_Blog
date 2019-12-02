using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using your_Blog.Models;

namespace your_Blog.Controllers.Article
{
    public class ArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Article
        public async Task<ActionResult> Index()
        {
            var articles = db.Articles.Include(a => a.Category);
            return View(await articles.ToListAsync());
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
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Tags = db.Tags.ToList();
            return View();
        }

        // POST: Article/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,ShortDescription,Date,CategoryId")] ArticleModel articleModel, int[] selectedTags)
        {
            if (ModelState.IsValid)
            {
                if (selectedTags != null)
                {
                    //получаем выбранные теги
                    foreach (var c in db.Tags.Where(co => selectedTags.Contains(co.Id)))
                    {
                        articleModel.Tags.Add(c);
                    }
                }
                db.Articles.Add(articleModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", articleModel.CategoryId);
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
            ViewBag.Tags = db.Tags.ToList();
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", articleModel.CategoryId);
            return View(articleModel);
        }

        // POST: Article/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ShortDescription,Date,CategoryId")] ArticleModel articleModel, int[] selectedTags)
        {
            if (ModelState.IsValid)
            {
                ArticleModel newArticleModel = db.Articles.Find(articleModel.Id);
                newArticleModel.Tags.Clear();
                if (selectedTags != null)
                {
                    //получаем выбранные теги
                    foreach (var c in db.Tags.Where(co => selectedTags.Contains(co.Id)))
                    {
                        newArticleModel.Tags.Add(c);
                    }
                }

                db.Entry(newArticleModel).State = EntityState.Modified;
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
    }
}
