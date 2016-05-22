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
                if (client.CheckEntries(Session["user"] as Users))
                {
                    ViewBag.Sum = client.GetSpendings("", Session["user"] as Users).Sum(s => s.Amount);
                    return View("Budget", client.GetSpendings("", Session["user"] as Users).ToList());
                }                    
                else
                {
                    ViewBag.Error = "NOENTRIES";
                    return View("Budget");
                }                    
            }
            
        }
    }
}