using Fiinasta.FiinastaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiinasta.Controllers
{
    public class SpendingsIncomeController : Controller
    {
        // GET: Spendings
        public ActionResult SpendingsIncome()
        {
            return View("SpendingsIncome");
        }
    }
}