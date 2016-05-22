using Fiinasta.FiinastaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Fiinasta.Controllers
{
    public class SpendingsIncomeController : Controller
    {
        // GET: Spendings
        public ActionResult SpendingsIncome()
        {
            ViewData["SpendingsIncome"] = "Spendings";
            using(FiinastaAPIClient client = new FiinastaAPIClient())
            {
                ViewData["category"] = new SelectList(client.GetCategories());
            }
            return View();
        }

        [HttpPost]
        public ActionResult SpendingsIncome(Spendings spendings, string rb, string category)
        {
            //booSpendings = ViewBag.Spendings;
            //booIncome = ViewBag.Income;
            using (FiinastaAPIClient client = new FiinastaAPIClient())  
            {
                if (rb == "spendings")
                {
                    Users user = Session["user"] as Users;
                    spendings.UserID = user.ID;
                    spendings.Category = category;
                    client.AddSpendings(spendings);
                }
                if (rb == "income")
                {
                    throw new NotImplementedException();
                }
                ViewData["Categories"] = new SelectList(client.GetCategories());
                ModelState.Clear();
            }            
            return View();
        }
    }
}