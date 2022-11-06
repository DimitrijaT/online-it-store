using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Http.Description;
using Grpc.Core;
using OnlineITStore.Models;

namespace OnlineITStore.Controllers
{
    public class Tbl_ProductAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Tbl_ProductAPI
        public IQueryable<Tbl_Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Tbl_ProductAPI/5
        [ResponseType(typeof(Tbl_Product))]
        public IHttpActionResult GetTbl_Product(int id)
        {
            Tbl_Product tbl_Product = db.Products.Find(id);
            if (tbl_Product == null)
            {
                return NotFound();
            }

            return Ok(tbl_Product);
        }

        // PUT: api/Tbl_ProductAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Product(int id, Tbl_Product tbl_Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Product.ProductId)
            {
                return BadRequest();
            }

            db.Entry(tbl_Product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_ProductExists(id))
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

        // POST: api/Tbl_ProductAPI
        [ResponseType(typeof(Tbl_Product))]
        public IHttpActionResult PostTbl_Product(Tbl_Product tbl_Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(tbl_Product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Product.ProductId }, tbl_Product);
        }

        // DELETE: api/Tbl_ProductAPI/5
        [ResponseType(typeof(Tbl_Product))]
        public IHttpActionResult DeleteTbl_Product(int id)
        {
            Tbl_Product tbl_Product = db.Products.Find(id);
            if (tbl_Product == null)
            {
                return NotFound();
            }

            string fullPath = null;
            try
            {
                fullPath = System.Web.HttpContext.Current.Server.MapPath(tbl_Product.ProductImage);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            catch
            {

            }


            db.Products.Remove(tbl_Product);
            db.SaveChanges();

            return Ok(tbl_Product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }
    }
}