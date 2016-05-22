using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiinasta.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Run(string code)
        {
            using (FiinastaAPI.FiinastaAPIClient client = new FiinastaAPI.FiinastaAPIClient())
            {
                var msg = client.GetMessageByID(code);
                return View("SystemMessage", msg);
            }
            
        }
    }
}