using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data.SqlClient;
using Hackathon.Domain;

namespace Hackathon.Presentation.Controllers
{
    public class HomeController : HackathonController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Data()
        {
            IQueryable<Transaction> transactions = Repository.Get<Transaction>();
            IQueryable<IGrouping<int, Transaction>> transactionsByHousehold = transactions
                .Where(t => t.description == "Wine")
                .Where(t => t.sales > 40)
                .GroupBy(t=>t.Household);
            List<Tuple<int, decimal>> householdSales = new List<Tuple<int,decimal>>();
            foreach (var grouping in transactionsByHousehold)
            {
                householdSales.Add(new Tuple<int, decimal>(grouping.Key, grouping.Sum(t=>(t.sales))));
            }
            return Json(householdSales, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DatabaseConnects()
        {
            Repository repository = new Repository();
            Transaction aTransaction = repository.Get<Transaction>(2);
            return Json(aTransaction, JsonRequestBehavior.AllowGet);
        }

    }
}
