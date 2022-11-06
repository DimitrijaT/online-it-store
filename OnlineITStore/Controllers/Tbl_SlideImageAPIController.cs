using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineITStore.Models;

namespace OnlineITStore.Controllers
{
    public class Tbl_SlideImageAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Tbl_SlideImageAPI
        public IQueryable<Tbl_SlideImage> GetSlideImages()
        {
            return db.SlideImages;
        }

        // GET: api/Tbl_SlideImageAPI/5
        [ResponseType(typeof(Tbl_SlideImage))]
        public IHttpActionResult GetTbl_SlideImage(int id)
        {
            Tbl_SlideImage tbl_SlideImage = db.SlideImages.Find(id);
            if (tbl_SlideImage == null)
            {
                return NotFound();
            }

            return Ok(tbl_SlideImage);
        }

        // PUT: api/Tbl_SlideImageAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_SlideImage(int id, Tbl_SlideImage tbl_SlideImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_SlideImage.SlideId)
            {
                return BadRequest();
            }

            db.Entry(tbl_SlideImage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_SlideImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tbl_SlideImageAPI
        [ResponseType(typeof(Tbl_SlideImage))]
        public IHttpActionResult PostTbl_SlideImage(Tbl_SlideImage tbl_SlideImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SlideImages.Add(tbl_SlideImage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_SlideImage.SlideId }, tbl_SlideImage);
        }

        // DELETE: api/Tbl_SlideImageAPI/5
        [ResponseType(typeof(Tbl_SlideImage))]
        public IHttpActionResult DeleteTbl_SlideImage(int id)
        {
            Tbl_SlideImage tbl_SlideImage = db.SlideImages.Find(id);
            if (tbl_SlideImage == null)
            {
                return NotFound();
            }


            string fullPath = null;
            try
            {
                fullPath = System.Web.HttpContext.Current.Server.MapPath(tbl_SlideImage.SlideImage);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            catch
            {


            }



            db.SlideImages.Remove(tbl_SlideImage);
            db.SaveChanges();

            return Ok(tbl_SlideImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_SlideImageExists(int id)
        {
            return db.SlideImages.Count(e => e.SlideId == id) > 0;
        }
    }
}