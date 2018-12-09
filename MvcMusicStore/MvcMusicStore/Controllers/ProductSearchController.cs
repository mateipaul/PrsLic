
using MvcMusicStore.Utilities.DatabaseUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class ProductSearchController : Controller
    {
        public ActionResult ProductSearch()
        {
            return View();
        }

       

        public ActionResult SearchProduct(string search)
        {
            var products = SearchDBUtilities.SearchProductsInDatabase(search.Replace(",",string.Empty));

            return View(products);
        }
    }
}
