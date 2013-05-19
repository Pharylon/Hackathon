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
    }
}
