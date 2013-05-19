using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hackathon.Domain;

namespace Hackathon.Presentation.Controllers
{
    public class PurchasesByTimeController : HackathonController
    {
        //
        // GET: /PurchasesByTime/

        public ActionResult Index()
        {
            return View();
        }
        //store or stores, 
        [HttpGet]
        public JsonResult GetData()
        {
            List<Transaction> transactions = Repository.Get<Transaction>()
                .Where(t => t.description=="Beer"||t.description=="Wine")
                .Take(2000).ToList();
            var purchasesByTime = new List<Tuple<decimal, decimal>>();
            purchasesByTime = transactions.Select(t => new Tuple<decimal, decimal>(t.sales, ((decimal)t.dateTime.Hour)+(((decimal)t.dateTime.Minute)/60)))
                .OrderByDescending(pbt => pbt.Item2).ToList();
            return Json(purchasesByTime, JsonRequestBehavior.AllowGet);
        }
    }
}
