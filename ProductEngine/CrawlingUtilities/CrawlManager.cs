﻿using DatabaseModels;
using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtilitiesModels;

namespace CrawlingUtilities
{
    internal class CrawlManager
    {
        private RetailerConfiguration RetailerConfig;
        private DbModelContext dbContext;
        private Guid retailerId;
        private RetailerCrawlProductCollection products;

        public CrawlManager(RetailerConfiguration retailer)
        {
            RetailerConfig = retailer;
        }


        public void StartCrawling()
        {
            Initialize();

            ParseProducts();


        }

        private void Initialize()
        {
            dbContext = new DbModelContext();

            retailerId = dbContext.Vanzator.Where(m => m.Nume.Equals(RetailerConfig.RetailerName)).Select(m => m.Id).FirstOrDefault();

            products = new RetailerCrawlProductCollection(RetailerConfig.RetailerName);

            var prods = dbContext.Produs.Where(m => m.Id_Vanzator == retailerId);

            products.AddRange(prods);

        }

        private void ParseProducts()
        {
            int counter = 0;
            do
            {
                CrawlProductParser parser = new CrawlProductParser(RetailerConfig);

                if (products.Count > 0)
                {
                    var currentProduct = products[counter];
                    parser.DownloadProduct(ref currentProduct);
                    counter++;

                    try
                    {
                        dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        GenericLogger.Error($"Error when trying to save changes for updating process for {currentProduct.Url}, \n ex : {ex.Message} \n {ex.StackTrace} \n {ex.InnerException.InnerException.Message}");
                    }
                }
            } while (counter < products.Count);
        }
    }
}