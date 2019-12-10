using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using your_Blog.Models;

namespace your_Blog.Controllers.Article
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: TagModels
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }

        // GET: TagModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TagModel tagModel = db.Tags.Find(id);

            if (tagModel == null)
            {
                return HttpNotFound();
            }

            return View(tagModel);
        }

        // GET: TagModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TagModel tagModel)
        {
            if (ModelState.IsValid)
            {
                db.Tags.Add(tagModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tagModel);
        }

        // GET: TagModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TagModel tagModel = db.Tags.Find(id);
            if (tagModel == null)
            {
                return HttpNotFound();
            }
            return View(tagModel);
        }

        // POST: TagModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TagModel tagModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tagModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tagModel);
        }

        // GET: TagModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TagModel tagModel = db.Tags.Find(id);
            if (tagModel == null)
            {
                return HttpNotFound();
            }
            return View(tagModel);
        }

        // POST: TagModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TagModel tagModel = db.Tags.Find(id);
            db.Tags.Remove(tagModel);
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
