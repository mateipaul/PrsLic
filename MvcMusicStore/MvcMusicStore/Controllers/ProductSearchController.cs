
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
    public class ProductSearchController : Controller
    {
        public ActionResult ProductSearch()
        {
            return View();
        }

       

        public ActionResult SearchProduct(string search)
        {
            var products = SearchDBUtilities.NewSearchProductsInDatabase(search.Replace(",",string.Empty).ToUpperInvariant());

            products = TryGetProducts(search.Replace(",", string.Empty).ToUpperInvariant());

            return View(products);
        }

        private IEnumerable<Produs> TryGetProducts(string v)
        {
            return SearchDBUtilities.SearchProductsInDatabase(v);
        }

        public ActionResult LineChart(string productId)
        {
            using (DbModelContext dbContext = new DbModelContext())
            {

                ArrayList xValues = new ArrayList();
                ArrayList yValues = new ArrayList();

                xValues.AddRange((from priceEvolution in dbContext.EvolutiaPretului where priceEvolution.Id_Produs == new Guid(productId) orderby priceEvolution.Data_Actualizare descending select priceEvolution.Data_Actualizare).Take(10).ToList());
                yValues.AddRange((from priceEvolution in dbContext.EvolutiaPretului where priceEvolution.Id_Produs == new Guid(productId) orderby priceEvolution.Data_Actualizare descending select priceEvolution.Pret).Take(10).ToList());

                new Chart(width: 300, height: 200, theme: ChartTheme.Blue).
                    AddTitle("Evolutia Pretului").
                    AddSeries(null, chartType: "Line", xValue: xValues, yValues: yValues).
                    Write("bmp");

            }
            return null;
        }
    }
}
