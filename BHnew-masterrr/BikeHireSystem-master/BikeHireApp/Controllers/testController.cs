using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeHireApp.Controllers
{
    public class testController : Controller
    {
        // GET: test
        public String Index()
        {
            return "every public method in of controller is callable";
        }
    }
}