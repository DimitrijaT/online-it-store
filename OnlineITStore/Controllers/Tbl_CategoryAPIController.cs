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
    public class Tbl_CategoryAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Tbl_CategoryAPI
        public IQueryable<Tbl_Category> GetCategories()
        {
            return db.Categories;
        }

        // GET: api/Tbl_CategoryAPI/5
        [ResponseType(typeof(Tbl_Category))]
        public IHttpActionResult GetTbl_Category(int id)
        {
            Tbl_Category tbl_Category = db.Categories.Find(id);
            if (tbl_Category == null)
            {
                return NotFound();
            }

            return Ok(tbl_Category);
        }

        // PUT: api/Tbl_CategoryAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Category(int id, Tbl_Category tbl_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Category.CategoryId)
            {
                return BadRequest();
            }

            db.Entry(tbl_Category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_CategoryExists(id))
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

        // POST: api/Tbl_CategoryAPI
        [ResponseType(typeof(Tbl_Category))]
        public IHttpActionResult PostTbl_Category(Tbl_Category tbl_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(tbl_Category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Category.CategoryId }, tbl_Category);
        }

        // DELETE: api/Tbl_CategoryAPI/5
        [ResponseType(typeof(Tbl_Category))]
        public IHttpActionResult DeleteTbl_Category(int id)
        {
            Tbl_Category tbl_Category = db.Categories.Find(id);
            if (tbl_Category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(tbl_Category);
            db.SaveChanges();

            return Ok(tbl_Category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_CategoryExists(int id)
        {
            return db.Categories.Count(e => e.CategoryId == id) > 0;
        }
    }
}