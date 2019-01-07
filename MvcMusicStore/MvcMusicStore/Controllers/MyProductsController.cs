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
        private List<Produs> products = new List<Produs>();

        DbModelContext dbContext = new DbModelContext();

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

        private List<Produs> GetProducts()
        {
            return new List<Produs>();
        }
    }
}