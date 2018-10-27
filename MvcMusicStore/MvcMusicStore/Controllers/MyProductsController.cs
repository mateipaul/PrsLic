using MvcMusicStore.Models;
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
        private List<ProductModel> products = new List<ProductModel>();

        // GET: MyProducts
        public ActionResult MyProducts()
        {
            products = GetProducts();

            return View(products);
        }

        public ActionResult LineChart(string productID)
        {
            products = GetProducts();

            ArrayList xValues = new ArrayList() { "1", "2", "3", "4", "5", "6", "7" };
            ArrayList yValues = new ArrayList() { Decimal.Parse(products.Where(m=>m.ID.Equals(productID)).Select(m=>m.Price).FirstOrDefault()), 20, 13, 40, 20, 2, 37 };


            new Chart(width: 200,height: 150, theme : ChartTheme.Blue)
                .AddTitle("Line Chart")
                .AddSeries("Default", chartType: "Line", xValue: xValues, yValues: yValues)
                .Write("bmp");

            return null;
        }

        private List<ProductModel> GetProducts()
        {
            List<ProductModel> tempProds = new List<ProductModel>();

            tempProds.Add(new ProductModel() { Description = "Prod 1", ID = "1", Name = "product 1", Price = "12,50" });
            tempProds.Add(new ProductModel() { Description = "Prod 2", ID = "2", Name = "product 2", Price = "22,50" });
            tempProds.Add(new ProductModel() { Description = "Prod 3", ID = "3", Name = "product 3", Price = "32,50" });
            tempProds.Add(new ProductModel() { Description = "Prod 4", ID = "4", Name = "product 4", Price = "42,50" });
            tempProds.Add(new ProductModel() { Description = "Prod 5", ID = "5", Name = "product 5", Price = "52,50" });
            tempProds.Add(new ProductModel() { Description = "Prod 6", ID = "6", Name = "product 6", Price = "62,50" });
            tempProds.Add(new ProductModel() { Description = "Prod 7", ID = "7", Name = "product 7", Price = "72,50" });
            tempProds.Add(new ProductModel() { Description = "Prod 8", ID = "8", Name = "product 8", Price = "82,50" });

            return tempProds;

        }
    }
}