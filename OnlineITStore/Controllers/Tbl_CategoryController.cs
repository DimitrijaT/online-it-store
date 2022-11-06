using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineITStore.Models;

namespace OnlineITStore.Controllers
{
    [Authorize(Roles = "Administrator,Shop Manager")]
    public class Tbl_CategoryController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Tbl_Category
        public ActionResult Index()
        {
            return View(_context.Categories.ToList());
        }

        // ==================================================CATEGORY==================================================

        public ActionResult Categories()
        {
            var all_categories = _context.Categories.Where(m => m.IsDelete == false).ToList();
            return View(all_categories);
        }

        // GET: Tbl_Category/Details/5
        public ActionResult CategoryDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Category tbl_Category = _context.Categories.Find(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }


        // GET: Tbl_Category/Create
        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: Tbl_Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory([Bind(Include = "CategoryId,CategoryName,IsActive,IsDelete")] Tbl_Category tbl_Category)
        {
            if (ModelState.IsValid)
            {
                tbl_Category.IsActive = true;
                tbl_Category.IsDelete = false;
                _context.Categories.Add(tbl_Category);
                _context.SaveChanges();
                return RedirectToAction("Categories");
            }

            return View(tbl_Category);
        }

        // GET: Tbl_Category/Edit/5
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Category tbl_Category = _context.Categories.Find(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }

        // POST: Tbl_Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "CategoryId,CategoryName,IsActive,IsDelete")] Tbl_Category tbl_Category)
        {
            if (ModelState.IsValid)
            {
                var category = _context.Categories.First(c => c.CategoryId == tbl_Category.CategoryId);
                category.CategoryName = tbl_Category.CategoryName;
                //_context.Entry(tbl_Category).State = EntityState.Modified;
                TryUpdateModel(category);
                _context.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View(tbl_Category);
        }



        // GET: Tbl_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Category tbl_Category = _context.Categories.Find(id);
            if (tbl_Category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Category);
        }

        // POST: Tbl_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Category tbl_Category = _context.Categories.Find(id);
            _context.Categories.Remove(tbl_Category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
