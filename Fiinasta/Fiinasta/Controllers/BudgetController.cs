using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fiinasta.FiinastaAPI;

namespace Fiinasta.Controllers
{
    public class BudgetController : Controller
    {
        // GET: Budget
        public ActionResult Run()
        {
            using (FiinastaAPIClient client = new FiinastaAPIClient())
            {
                return View("Budget", client.GetSpendings("",Session["user"] as Users).ToList());
            }
            
        }
        [HttpPost]
        public void Budget(string criteria)
        {
            Url.Action("Run", "Search", new
            {
                @criteria = criteria
            });
        }
    }
}