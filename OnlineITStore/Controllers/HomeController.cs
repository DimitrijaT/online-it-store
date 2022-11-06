using Microsoft.AspNetCore.Mvc;
using OnlineITStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace OnlineITStore.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.ShowPresent = true;
            var model = _context.Products.ToList();
            ViewBag.categoryList = _context.Categories.ToList().Select(s => s.CategoryName);
            ViewBag.slideImages = _context.SlideImages.ToList();
            //(ViewBag.categoryList as List<string>).Add("All");
            return View("Index", model);
        }

        public ActionResult DailyDeal()
        {
            var product = GetDailyDeal();
            return PartialView("_DailyDeal", product);
        }

        public Tbl_Product GetDailyDeal()
        {
            var product = _context.Products.Where(m => m.IsOnSale == true).OrderBy(a => System.Guid.NewGuid()).First();
            return product;
        }

        //[Route("home/index/{category}/{product}")]
        [HttpPost]
        public ActionResult Index(string category, string product)
        {
            ViewBag.ShowPresent = false;
            var categoryList = _context.Categories.ToList();

            var productList = from m in _context.Products
                              select m;

            if (!String.IsNullOrEmpty(product))
            {
                productList = productList.Where(s => s.ProductName.Contains(product));
            }

            if (!String.IsNullOrEmpty(category) && category != "" && category != "All Trending")
            {
                
                int? categoryIdToFind = null;
                foreach (var item in categoryList)
                {
                    if (item.CategoryName == category)
                    {
                        categoryIdToFind = item.CategoryId;
                    }
                }

                productList = productList.Where(x => x.CategoryId == categoryIdToFind);
            }
            if (category == "")
            {
                ViewBag.ShowPresent = true;
            }

            ViewBag.categoryList = _context.Categories.ToList().Select(s => s.CategoryName);
            ViewBag.slideImages = _context.SlideImages.ToList();
            return View("Index", productList.ToList());
        }



        public ActionResult ProductListing()
        {
            return View(_context.Products.ToList());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null || ((List<Item>)Session["cart"]).Count != 0)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = _context.Products.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 1)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        else
                        {
                            cart.Remove(item);
                        }
                        break;
                    }
                    if (cart.Count == 0)
                    {
                        cart = null;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }

        public ActionResult AddToCart(int productId, string url, bool onSale = false)
        {
            if (Session["cart"] == null || ((List<Item>)Session["cart"]).Count == 0)
            {
                List<Item> cart = new List<Item>();
                var product = _context.Products.Find(productId);
                //if (onSale == true)
                //{
                //    product.Price = product.Price * (decimal)0.5;
                //}
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = _context.Products.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            if (url == "Checkout" || url == "Details")
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return PartialView("_AddToCart");
            }
            //return Redirect(url);

        }

        public ActionResult RemoveFromCart(int productId, string url)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            if (cart.Count == 0)
            {
                cart = null;
            }
            Session["cart"] = cart;

            //url = "~" + url.Substring(7, url.Length - 14);

            //return Redirect(url);
            return Redirect(Request.UrlReferrer.ToString());
            //return PartialView("_AddToCart");
        }


        public ActionResult CheckoutDetails()
        {

            return View();
        }

        public ActionResult Checkout()
        {

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Payment()
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (Item item in cart)
            {

                var product = _context.Products.Where(m => m.ProductId == item.Product.ProductId).FirstOrDefault();
                product.Quantity -= item.Quantity;
                if (product.Quantity == 0)
                {
                    product.IsActive = false;
                }
                TryUpdateModel(product);
                _context.SaveChanges();
                //cart.Remove(item);

            }
            cart = null;
            Session["cart"] = cart;


            var model = _context.Products.ToList();
            ViewBag.categoryList = _context.Categories.ToList().Select(s => s.CategoryName);
            ViewBag.ShowPresent = true;
            ViewBag.slideImages = _context.SlideImages.ToList();
            return View("Index", model);
        }



    }
}