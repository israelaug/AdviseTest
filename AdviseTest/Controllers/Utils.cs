using AdviseTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdviseTest.Controllers
{
    public class Utils
    {
        private AdventureWorks2012Entities db = new AdventureWorks2012Entities();

        #region Model

        public List<ProductModel_min> PageModelResult(int page = 1, int rowsPage = 20)

        // A principio foi comentada a listagem de produtos dentro dos modelos.
        // Pois isso deixaria as consultas lentas e pesadas. Cada modelo pode ter diversos subprodutos.
        // No desafio não foi pedido que gereciassemos de forma relativa, cada produto com seu modelo.

        {
            int position = (rowsPage * (page - 1)) + 1;

            List<ProductModel_min> modelList = (from pm in db.ProductModel
                                            .OrderBy(x => x.Name)
                                            .Skip(position)
                                            .Take((position -1) + rowsPage)
                                                select new ProductModel_min
                                                {
                                                    ProductModelID = pm.ProductModelID,
                                                    Name = pm.Name,
                                                    CatalogDescription = pm.CatalogDescription,
                                                    Instructions = pm.Instructions,
                                                    ModifiedDate = pm.ModifiedDate
                                                }).ToList();

            return modelList;
        }

        public int NewModelID()
        {
            // generating a new ID

                int newID = 0;

                try
                {
                    newID = db.ProductModel.Max(u => (int) u.ProductModelID);
                }
                catch (Exception)
                {
                    newID += 1;
                }

            return newID;
        }

        #endregion


        #region Product

        public List<Product_min> PageProductResult(int page = 1, int rowsPage = 20)
        {

            int position = (rowsPage * (page - 1)) + 1;

            List<Product> productList = (   from p in db.Product
                                            .OrderBy(x => x.Name)
                                            .Skip(position)
                                            .Take((position -1) + rowsPage)
                                            select new Product_min
                                            {
                                                ProductID = p.ProductID,
                                                Name = p.Name,
                                                CatalogDescription = p.CatalogDescription,
                                                Instructions = p.Instructions,
                                                ModifiedDate = p.ModifiedDate
                                            }).ToList();

        }

        public int NewProductID()
        {
            // generating a new ID

            int newID = 0;

            try
            {
                newID = db.Product.Max(u => (int)u.ProductID);
            }
            catch (Exception)
            {
                newID += 1;
            }

            return newID;
        }

        #endregion

    }
}