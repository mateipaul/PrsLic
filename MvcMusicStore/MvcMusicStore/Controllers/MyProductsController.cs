using MvcMusicStore.Utilities.DatabaseUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class MyProductsController : Controller
    {
        private List<Product> products = new List<Product>();

        DbConnectionString dbContext = new DbConnectionString();

        // GET: MyProducts
        public ActionResult MyProducts()
        {
            products = GetProducts();

            return View(products);
        }

        public ActionResult LineChart(string productID)
        {
            return null;
        }

        private List<Product> GetProducts()
        {
            return new List<Product>();
        }
    }
}