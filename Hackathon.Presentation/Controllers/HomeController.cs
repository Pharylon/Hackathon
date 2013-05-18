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
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Data()
        {
            Element hydrogen = new Element() { Symbol = "H", AtomicMass = 1.07947 };
            Element helium = new Element() { Symbol = "He", AtomicMass = 4.002602 };
            Element lithium = new Element() { Symbol = "Li", AtomicMass = 6.941 };
           List<Element> myElements = new List<Element>(){hydrogen, helium, lithium};
            return Json(myElements, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DatabaseConnects()
        {
            //SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["hackathonConnectionString"].ToString());
            Repository repository = new Repository();
            Transaction aTransaction = repository.Get<Transaction>(2);
            return Json(aTransaction, JsonRequestBehavior.AllowGet);
        }

    }
        public class Element
        {
            public string Symbol { get; set; }
            public double AtomicMass { get; set; }
        }
}
