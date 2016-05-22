using Fiinasta.FiinastaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiinasta.Controllers
{
    public class StatisticsController : Controller
    {
        private List<Spendings> entries;
        // GET: Statistics
        public ActionResult Statistics()
        {
            using (FiinastaAPIClient client = new FiinastaAPIClient())
            {
                if(client.CheckEntries(Session["user"] as Users))
                {
                    entries = client.GetSpendings("", Session["user"] as Users).ToList();
                    ViewBag.AvgSpendings = (entries.Sum(e => e.Amount) / entries.Count()).ToString();
                }
                else
                {
                    ViewBag.Error = "NOENTRIES";
                }
            }
            return View();
        }

        //public ActionResult GetStatistics(DateTime periodFrom, DateTime periodTo)
        [HttpPost]
        public ActionResult Statistics(DateTime periodFrom, DateTime periodTo)
        {
            ViewBag.Period = "Statistika nuo " + periodFrom.ToShortDateString() + " iki " + periodTo.ToShortDateString();
            using (FiinastaAPIClient client = new FiinastaAPIClient())
            {
                if (client.CheckEntries(Session["user"] as Users))
                {
                    entries = client.GetSpendings(periodFrom.ToString("yyyy-MM-dd") + "_"+ periodTo.ToString("yyyy-MM-dd"), Session["user"] as Users).ToList();
                    ViewBag.AvgSpendings = (entries.Sum(e => e.Amount) / entries.Count()).ToString();
                }
                else
                {
                    ViewBag.Error = "NOENTRIES";
                }
                ModelState.Clear();
            }
            return View("Statistics");
        }
    }
}