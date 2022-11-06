using OnlineITStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineITStore.Controllers
{
    [Authorize(Roles ="Administrator,Shop Manager")]
    public class AdminController : Controller
    {

        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }


    }
}