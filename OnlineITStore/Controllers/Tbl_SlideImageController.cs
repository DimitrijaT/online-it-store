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
    public class Tbl_SlideImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tbl_SlideImage
        public ActionResult Index()
        {
            return View(db.SlideImages.ToList());
        }
        public ActionResult SlideImages()
        {
            return View(db.SlideImages.ToList());
        }

        // GET: Tbl_SlideImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_SlideImage tbl_SlideImage = db.SlideImages.Find(id);
            if (tbl_SlideImage == null)
            {
                return HttpNotFound();
            }
            return View(tbl_SlideImage);
        }

        // GET: Tbl_SlideImage/Create
        public ActionResult CreateSlideImage()
        {
            return View();
        }

        // POST: Tbl_SlideImage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSlideImage([Bind(Include = "SlideId,SlideTitle,SlideUrl,SlideImage,IsActive,SlideDescription")] Tbl_SlideImage tbl_SlideImage, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Images/Carousel/"), pic);
                    file.SaveAs(path);
                    tbl_SlideImage.SlideImage = "~/Images/Carousel/" + pic;
                }

                if (tbl_SlideImage.SlideTitle == null)
                {
                    tbl_SlideImage.SlideTitle = "";
                }
                if (tbl_SlideImage.SlideDescription == null)
                {
                    tbl_SlideImage.SlideDescription = "";
                }

                if ((file == null || file.ContentLength > 0) && (tbl_SlideImage.SlideImage == null || tbl_SlideImage.SlideImage == ""))
                {
                    tbl_SlideImage.SlideImage = "https://www.ncenet.com/wp-content/uploads/2020/04/No-image-found.jpg";
                }

                db.SlideImages.Add(tbl_SlideImage);
                db.SaveChanges();
                return RedirectToAction("SlideImages");
            }

            return View(tbl_SlideImage);
        }

        // GET: Tbl_SlideImage/Edit/5
        public ActionResult EditSlideImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_SlideImage tbl_SlideImage = db.SlideImages.Find(id);
            if (tbl_SlideImage == null)
            {
                return HttpNotFound();
            }
            return View(tbl_SlideImage);
        }

        // POST: Tbl_SlideImage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSlideImage([Bind(Include = "SlideId,SlideTitle,SlideUrl,SlideImage,IsActive,SlideDescription")] Tbl_SlideImage tbl_SlideImage, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Images/Carousel/"), pic);
                    file.SaveAs(path);
                    tbl_SlideImage.SlideImage = "~/Images/Carousel/" + pic;
                }
                if (tbl_SlideImage.SlideTitle == null)
                {
                    tbl_SlideImage.SlideTitle = "";
                }
                if (tbl_SlideImage.SlideDescription == null)
                {
                    tbl_SlideImage.SlideDescription = "";
                }

                if ((file == null || file.ContentLength > 0) && (tbl_SlideImage.SlideImage == null || tbl_SlideImage.SlideImage == ""))
                {
                    tbl_SlideImage.SlideImage = "https://www.ncenet.com/wp-content/uploads/2020/04/No-image-found.jpg";
                }

                db.Entry(tbl_SlideImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SlideImages");
            }
            return View(tbl_SlideImage);
        }

        // GET: Tbl_SlideImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_SlideImage tbl_SlideImage = db.SlideImages.Find(id);
            if (tbl_SlideImage == null)
            {
                return HttpNotFound();
            }
            return View(tbl_SlideImage);
        }

        // POST: Tbl_SlideImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_SlideImage tbl_SlideImage = db.SlideImages.Find(id);
            db.SlideImages.Remove(tbl_SlideImage);
            db.SaveChanges();
            return RedirectToAction("SlideImages");
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
