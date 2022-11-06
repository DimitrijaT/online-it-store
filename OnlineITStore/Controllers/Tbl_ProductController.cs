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
    public class Tbl_ProductController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        [Authorize(Roles = "Administrator,Shop Manager")]
        // GET: Tbl_Product
        public ActionResult Index()
        {
            var products = _context.Products.Include(t => t.Tbl_Category);
            return View(products.ToList());
        }

        // ==================================================PRODUCT==================================================
        [Authorize(Roles = "Administrator,Shop Manager")]
        public ActionResult Products()
        {
            var all_products = _context.Products.Where(m => m.IsDelete == false).ToList();
            return View(all_products);
        }

        [Authorize(Roles = "Administrator,Shop Manager")]
        // GET: Tbl_Product/Details/5
        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = _context.Products.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = _context.Products.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }
        [Authorize(Roles = "Administrator,Shop Manager")]
        // GET: Tbl_Product/Create
        public ActionResult CreateProduct()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Tbl_Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator,Shop Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "ProductId,ProductName,CategoryId,IsActive,IsDelete,CreatedDate,ModifiedDate,ProductImage,IsFeatured,Quantity,Price,IsOnSale,ProductDescription,SaleModifier")] Tbl_Product tbl_Product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                /* FOR UPLOADING PICTURE ON SERVER, OTHERWISE USE HOSTED */
                if (file != null && file.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Images/Products/"), pic);
                    file.SaveAs(path);
                    tbl_Product.ProductImage = "~/Images/Products/" + pic;
                }

                if ((file == null || file.ContentLength > 0) && (tbl_Product.ProductImage == null || tbl_Product.ProductImage == ""))
                {
                    tbl_Product.ProductImage = "https://www.ncenet.com/wp-content/uploads/2020/04/No-image-found.jpg";
                }

                tbl_Product.ModifiedDate = DateTime.Now;
                tbl_Product.CreatedDate = DateTime.Now;
                tbl_Product.IsActive = true;
                tbl_Product.IsDelete = false;
                _context.Products.Add(tbl_Product);
                _context.SaveChanges();
                return RedirectToAction("Products");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", tbl_Product.CategoryId);
            return View(tbl_Product);
        }

        // GET: Tbl_Product/Edit/5
        [Authorize(Roles = "Administrator,Shop Manager")]
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = _context.Products.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", tbl_Product.CategoryId);
            return View(tbl_Product);
        }

        // POST: Tbl_Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator,Shop Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "ProductId,ProductName,CategoryId,IsActive,IsDelete,CreatedDate,ModifiedDate,ProductImage,IsFeatured,Quantity,Price,IsOnSale,ProductDescription,SaleModifier")] Tbl_Product tbl_Product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                /* FOR UPLOADING PICTURE ON SERVER, OTHERWISE USE HOSTED */
                if (file != null && file.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Images/Products/"), pic);
                    file.SaveAs(path);
                    tbl_Product.ProductImage = "~/Images/Products/" + pic;
                }

                if ((file == null || file.ContentLength > 0) && (tbl_Product.ProductImage == null || tbl_Product.ProductImage == ""))
                {
                    tbl_Product.ProductImage = "https://www.ncenet.com/wp-content/uploads/2020/04/No-image-found.jpg";
                }

                tbl_Product.ModifiedDate = DateTime.Now;
                _context.Entry(tbl_Product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Products");
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", tbl_Product.CategoryId);
            return View(tbl_Product);
        }


        [Authorize(Roles = "Administrator,Shop Manager")]
        // GET: Tbl_Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product tbl_Product = _context.Products.Find(id);
            if (tbl_Product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Product);
        }
        [Authorize(Roles = "Administrator,Shop Manager")]
        // POST: Tbl_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Product tbl_Product = _context.Products.Find(id);
            var fullPath = tbl_Product.ProductImage;
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            _context.Products.Remove(tbl_Product);
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
