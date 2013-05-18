using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hackathon.Domain;

namespace Hackathon.Presentation.Controllers
{
    public abstract class HackathonController : Controller
    {
        public Repository Repository
        {
            get
            {
                if (_Repository == null)
                {
                    _Repository = new Repository();
                }
                return _Repository;
            }
        }
        private Repository _Repository;
    }
}
