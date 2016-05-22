using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fiinasta.FiinastaAPI;

namespace Fiinasta.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Run(string criteria)
        {
            using (FiinastaAPIClient client = new FiinastaAPIClient())
            {
                var a = client.Search(criteria, Session["user"] as Users);
                return View("Budget", client.Search(criteria, Session["user"] as Users));
            }
        }
    }
}