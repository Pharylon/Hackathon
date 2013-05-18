using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hackathon.Domain;

namespace Hackathon.Presentation.Controllers
{
    public class SalesPerHouseholdController : HackathonController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ByDescription(string description)
        {
            IQueryable<Transaction> transactions = Repository.Get<Transaction>();
            IEnumerable<decimal> orderedSalesPerHousehold = transactions
                .Where(t => t.description == description)
                .GroupBy(t => t.Household)
                .Select(th => th.Sum(t => t.sales))
                .OrderBy(t => t);
            int householdNumber = 0;
            var householdSales = new List<Tuple<int, decimal>>();
            foreach(decimal saleAmount in orderedSalesPerHousehold)
            {
                householdSales.Add(new Tuple<int, decimal>(householdNumber, saleAmount));
                householdNumber++;
            }

            return Json(householdSales, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ByStore()
        {
            throw new NotImplementedException();
        }
    }
}
