using AdviseTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdviseTest.Controllers
{
    public class ProductModelController : ApiController
    {

        private AdventureWorks2012Entities db = new AdventureWorks2012Entities();

        Utils tools = new Utils();

        // A principio foi comentada a listagem de produtos dentro dos modelos.
        // Pois isso deixaria as consultas lentas e pesadas. Cada modelo pode ter diversos subprodutos.
        // No desafio não foi pedido que gereciassemos de forma relativa, cada produto com seu modelo.

        public List<ProductModel_min> GetModelList(int id = 0)
        {

            List<ProductModel_min> modelList = new List<ProductModel_min>();

            if (id == 0)
            {

                modelList = tools.PageModelResult(1);

            }
            else
            {
                modelList = (from pm in db.ProductModel
                             where (pm.ProductModelID == id)
                            select new ProductModel_min
                            {
                                ProductModelID = pm.ProductModelID,
                                Name = pm.Name,
                                CatalogDescription = pm.CatalogDescription,
                                Instructions = pm.Instructions,
                                ModifiedDate = pm.ModifiedDate
                            }).ToList();

            }

            return modelList;
        }

        public HttpResponseMessage PostModel([FromBody]ProductModel_min pm)
        {

            if (string.IsNullOrEmpty(pm.Name))
            {
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);

            }
            else
            {

                ProductModel newPm = new ProductModel();

                newPm.ProductModelID = tools.NewModelID();
                newPm.Name = pm.Name;
                newPm.CatalogDescription = pm.CatalogDescription;
                newPm.Instructions = pm.Instructions;
                newPm.rowguid = pm.rowguid;
                newPm.ModifiedDate = DateTime.Now;
                newPm.Product = null;

                
                db.ProductModel.Add(newPm);
                db.SaveChanges();

                var response = Request.CreateResponse<ProductModel>(HttpStatusCode.Created, newPm);
                return response;
            }
        }


        public void PutModel([FromBody]ProductModel pm)
        {
            try
            {
                ProductModel model = db.ProductModel.First(x => x.ProductModelID == pm.ProductModelID);

                model.Name = pm.Name;
                model.CatalogDescription = pm.CatalogDescription;
                model.Instructions = pm.Instructions;
                model.ModifiedDate = DateTime.Now;

                db.SaveChanges();

            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.NotModified);
            }

        }


        public void DeleteModel(int id)
        {
            try
            {
                db.ProductModel.First(x => x.ProductModelID == id).Active = false;

                db.SaveChanges();


                //db.ProductModel.Remove(
                //    db.ProductModel.Single(x => x.ProductModelID == id));

                // exclusao por nome
                //models.RemoveAt(
                //        models.IndexOf(
                //            models.First(x => x.Equals(nome))));

            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

    }
}
