using invoice_ms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace invoice_ms.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            ViewBag.list = Invoice.GetAll();
            ViewBag.Clients = Client.GetAll();
            ViewBag.Banks = Bank.GetAll();
            ViewBag.Products = Product.GetAll();
            return View();
        }
        public ActionResult GenerateInvoice(int id) {
            var clients = Client.GetAll();
            var banks = Bank.GetAll();
            var invoice= Invoice.getInvoice(id);
            var products = Product.GetAll();
            var validProducts = new List<Product>();
            ViewBag.invoice = invoice;
            foreach (var product in products)
            {
                foreach (var list in invoice.products)
                {
                    if (product.Id == list.Id) {
                        validProducts.Add(product);
                    }
                }
            }
            ViewBag.products = validProducts;
            foreach (var client in clients)
            {
                if (invoice.Client_id == client.Id) {
                    client.Contactinfo = Client.GetContactInfo(client.Id);
                    ViewBag.client = client;
                    break;
                }
            }
            foreach (var bank in banks)
            {
                if (invoice.Bank_id == bank.Id)
                {
                    ViewBag.bank = bank;
                    break;
                }
            }
            return View("Generate");
        }
        [HttpGet]
        public JsonResult getInvoice(int id)
        {
            return Json(Invoice.getInvoice(id), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult addInvoice(Invoice invoice)
        {
            var result = Invoice.InsertInvoice(invoice);
            //return View();
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
        [HttpPut]
        public ActionResult updateInvoice(Invoice newInvoice)
        {
            var result = Invoice.UpdateInvoice(newInvoice);
            //return View();
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
        [HttpDelete]
        public ActionResult DeleteInvoice(int id)
        {
            var result = Invoice.deleteInvoice(id);
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

        }
    }
}