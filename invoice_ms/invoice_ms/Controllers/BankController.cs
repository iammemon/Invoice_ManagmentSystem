using invoice_ms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace invoice_ms.Controllers
{
    public class BankController : Controller
    {
        // GET: Bank
        public ActionResult Index()
        {
            ViewBag.list = Bank.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult AddBank(Bank bankDetail) {

           var result= Bank.AddBank(bankDetail);
            //return View();
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

        }

        [HttpDelete]
        public ActionResult DeleteBank(int id) {
            var result = Bank.deleteBank(id);
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

        }

        [HttpPut]
        public ActionResult UpdateBank(Bank updatedbank) {
            var result = Bank.updateBank(updatedbank);
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }


    }
}