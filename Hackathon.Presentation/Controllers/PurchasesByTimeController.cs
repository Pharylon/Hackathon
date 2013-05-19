using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hackathon.Domain;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;
using FluentJson;
using System.Web.Script.Serialization;

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
                .Skip(2000).Take(2000).ToList();
            var purchasesByTime = new List<Tuple<decimal, decimal>>();
            purchasesByTime = transactions.Select(t => new Tuple<decimal, decimal>(t.sales, ((decimal)t.dateTime.Hour)+(((decimal)t.dateTime.Minute)/60)))
                .OrderByDescending(pbt => pbt.Item2).ToList();
            return Json(purchasesByTime, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetFilteredData(string filters)
        {
            List<test> theFilters = (List<test>)Newtonsoft.Json.JsonConvert.DeserializeObject(Request["filters"], typeof(List<test>));
            var jsonSerialization = new JavaScriptSerializer();

            IQueryable<Transaction> transactions = Repository.Get<Transaction>();
            int tryParseIntOut = 0;
            foreach(var pair in theFilters)
            {
                if(pair.Item1=="description")
                {
                    transactions=transactions.Where(t=> t.description== pair.Item2);
                }
                if (pair.Item1 == "store")
                {
                    if(int.TryParse(pair.Item2,out tryParseIntOut)){
                        transactions = transactions.Where(t => t.store == tryParseIntOut);
                    }
                }
                if (pair.Item1 == "outlaymin")
                {
                    //if (int.TryParse(pair.Item2, out tryParseIntOut))
                    //{
                    //    transactions = transactions.Where(t => t.store == tryParseIntOut);
                    //}
                }
                if (pair.Item1 == "outlaymax")
                {
                    //if (int.TryParse(pair.Item2, out tryParseIntOut))
                    //{
                    //    transactions = transactions.Where(t => t.store == tryParseIntOut);
                    //}
                }
            }
            transactions = transactions
                .Take(10000);
            var purchasesByTime = new List<Tuple<decimal, decimal>>();
            purchasesByTime = transactions.Select(t => new Tuple<decimal, decimal>(t.sales, ((decimal)t.dateTime.Hour) + (((decimal)t.dateTime.Minute) / 60)))
                .OrderByDescending(pbt => pbt.Item2).ToList();
            return Json(purchasesByTime, JsonRequestBehavior.AllowGet);
        }
        public class test
        {
            public string Item1 { get; set; }
            public string Item2 { get; set; }
        }  
    }
}
