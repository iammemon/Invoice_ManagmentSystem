using invoice_ms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace invoice_ms.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.list = Product.GetAll();
            return View();
        }
        [HttpPost]
        public ActionResult Addproduct(Product product)
        {

            var result = Product.AddProduct(product);
            //return View();
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

        }
        [HttpPut]
        public ActionResult UpdateProduct(Product updatedProduct)
        {
            var result = Product.updateProduct(updatedProduct);
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
        [HttpDelete]
        public ActionResult DeleteProduct(int id)
        {
            var result = Product.deleteProduct(id);
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

        }
    }
}