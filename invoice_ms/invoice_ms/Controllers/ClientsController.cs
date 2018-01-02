using invoice_ms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace invoice_ms.Controllers
{
    public class ClientsController : Controller
    {
        // GET: Clients
        public ActionResult Index()
        {
            ViewBag.list = Client.GetAll();
            
            return View();
        }
        [HttpGet]
        public JsonResult ContactInfo(int id) {
            return Json(Client.GetContactInfo(id), JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        public ActionResult addClient(Client client) {
            var result = Client.InsertClient(client);
            //return View();
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
        [HttpPut]
        public ActionResult updateClient(Client client)
        {
            var result = Client.UpdateClient(client);
            //return View();
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
        [HttpDelete]
        public ActionResult DeleteClient(int id)
        {
            var result = Client.deleteClient(id);
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

        }





    }
}