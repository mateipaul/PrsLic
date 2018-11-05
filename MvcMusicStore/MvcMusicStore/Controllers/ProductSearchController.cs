using MvcMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class ProductSearchController : Controller
    {
        List<Retailer> retailerList;
        // GET: ProductSearch
        public ActionResult ProductSearch()
        {
            retailerList = GetRetailersList();
            return View(retailerList);
        }

        private List<Retailer> GetRetailersList()
        {
            var tempRetailerList = new List<Retailer>();

            tempRetailerList.Add(new Retailer() { RetailerName = "Cel.ro", Url = "https://www.cel.ro", Logo = "../Images/cel.png" });
            tempRetailerList.Add(new Retailer() { RetailerName = "Media Galaxy", Url = "https://www.mediagalaxy.ro", Logo = "../Images/MediaGalaxy.png" });



            return tempRetailerList;
        }
    }
}
