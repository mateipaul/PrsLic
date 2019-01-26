using CelParser;
using DatabaseModels;
using Interfaces;
using Logger;
using PcGarageParser;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UtilitiesModels;

namespace CrawlingUtilities
{
    internal class CrawlManager
    {
        private RetailerConfiguration RetailerConfig;
        private Guid retailerId;
        private RetailerCrawlProductCollection products;
        private IParseProduct parser;

        public CrawlManager(RetailerConfiguration retailer)
        {
            RetailerConfig = retailer;
        }


        public void StartCrawling()
        {
            using (var dbContext = new DbModelContext())
            {
                Initialize(dbContext);

                ParseProducts(dbContext);
            }

        }

        private void Initialize(DbModelContext dbContext)
        {
            GetProductsFromDB(dbContext);

            GetParser();
            
        }

        private void GetParser()
        {
            switch (RetailerConfig.RetailerName)
            {

                case "Cel":
                    {
                        parser = new CelProductParser(RetailerConfig);
                        break;
                    }
                case "PcGarage":
                    {
                        parser = new PcGarageProductParser(RetailerConfig);
                        break;
                    }

                default:
                    break;
            }

        }

        private void GetProductsFromDB(DbModelContext dbContext)
        {

            retailerId = dbContext.Vanzator.Where(m => m.Nume.Equals(RetailerConfig.RetailerName)).Select(m => m.Id).FirstOrDefault();

            products = new RetailerCrawlProductCollection(RetailerConfig.RetailerName);

            var prods = (from prod in dbContext.Produs
                         join evol in dbContext.EvolutiaPretului on prod.Id equals evol.Id_Produs
                         join retailer in dbContext.Vanzator on prod.Id_Vanzator equals retailer.Id
                         where retailer.Id.Equals(retailerId)
                         select prod).Include(p => p.EvolutiaPretului).ToList();



            products.AddRange(prods);
        }

        private void ParseProducts(DbModelContext dbContext)
        {
            int counter = 0;
            
                do
                {
                   

                    if (products.Count > 0)
                    {
                        var currentProduct = products[counter];
                        parser.GetProduct(ref currentProduct);
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