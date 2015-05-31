using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crm.Site.Controllers
{
    using Crm.IServices;
    public class HomeController : Controller
    {
        IsysKeyValueServices keyvalSer;
        public HomeController(IsysKeyValueServices ser)
        {
            keyvalSer = ser;
        }
        public ActionResult Index()
        {
            var name = keyvalSer.QueryWhere(s => true).FirstOrDefault().KName;
            return Content(name);
        }

    }
}
