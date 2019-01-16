
using MvcMusicStore.Utilities;
using MvcMusicStore.Utilities.DatabaseUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcMusicStore.Controllers
{
    public class ProductSearchController : Controller
    {
        public ActionResult ProductSearch()
        {
            return View();
        }

       

        public ActionResult SearchProduct(string search,string opt_radio)
        {
            var products = SearchDBUtilities.NewSearchProductsInDatabase(search.Replace(",",string.Empty).ToUpperInvariant(),opt_radio);

            products = TryGetProducts(search.Replace(",", string.Empty).ToUpperInvariant(),opt_radio);

            return View(products);
        }

        [HttpPost]
        public ActionResult FollowProduct(Guid productId)
        {
           
            Produs product = SearchDBUtilities.GetProductOnId(productId);

            return View(product);
        }

        [HttpGet]
        public ActionResult SetAllert(string price,Guid productId)
        {
            Guid userId = CookieUtilities.GetUserIdFromCookie(Request);

            ProductFollowUtilities.AddProductAllert(productId,userId,price);

            return RedirectToAction("MyProducts", "MyProducts");
        }

        private IEnumerable<Produs> TryGetProducts(string v,string opt)
        {
            return SearchDBUtilities.SearchProductsInDatabase(v,opt);
        }

        public ActionResult LineChart(string productId)
        {
            using (DbModelContext dbContext = new DbModelContext())
            {

                ArrayList xValues = new ArrayList();
                ArrayList yValues = new ArrayList();

                xValues.AddRange((from priceEvolution in dbContext.EvolutiaPretului where priceEvolution.Id_Produs == new Guid(productId) orderby priceEvolution.Data_Actualizare descending select priceEvolution.Data_Actualizare).Take(10).ToList());
                yValues.AddRange((from priceEvolution in dbContext.EvolutiaPretului where priceEvolution.Id_Produs == new Guid(productId) orderby priceEvolution.Data_Actualizare descending select priceEvolution.Pret).Take(10).ToList());

                new Chart(width: 200, height: 180, theme: ChartTheme.Blue).
                    AddTitle("Evolutia Pretului").
                    AddSeries(null, chartType: "Line", xValue: xValues, yValues: yValues).
                    Write("bmp");
            }
            return null;
        }
    }
}
