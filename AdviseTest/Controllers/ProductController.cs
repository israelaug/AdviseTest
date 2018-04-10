using AdviseTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdviseTest.Controllers
{
    public class ProductController : ApiController
    {
        // Database Connection
        private AdventureWorks2012Entities db = new AdventureWorks2012Entities();

        Utils tools = new Utils();

        // Relatorio
        [Route("api/v1/relatorio")]
        public List<uspGetProducts_Result> GetProductList(int page = 1, int rowsPage = 20)
        {
            var procedureResult = db.uspGetProducts(null, page, rowsPage, true, false);

            return procedureResult;
        }

        public List<Product> GetProduct(int id = 0)
        {
            List<Product> productList = new List<Product>();

            if (id == 0)
            {
                productList = tools.PageProductResult(1);

            } else
            {
                productList.Add(db.Product.Find(id));
            }
            return productList;
        }


        public HttpResponseMessage PostProduct([FromBody]Product p)
        {
            if (string.IsNullOrEmpty(p.Name))
            {
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);

            } else
            {
                p.ProductID = tools.NewProductID();
                db.Product.Add(p);
                db.SaveChanges();

                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, p);
                return response;
            }
        }

        public void PutProduto([FromBody]Product p)
        {
            try
            {
                Product product = db.Product.First(x => x.ProductID == p.ProductID);

                product = p;

                product.ModifiedDate = DateTime.Now;

                db.SaveChanges();

            } catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.NotModified);
            }

        }

        public void DeleteProduct(int id)
        {
            try
            {
                db.Product.First(x => x.ProductID == id).Active = false;

                db.SaveChanges();

                //db.Product.ToList().RemoveAt(id);

                // exclusao por nome
                //products.RemoveAt(
                //        products.IndexOf(
                //            products.First(x => x.Equals(nome))));

            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
