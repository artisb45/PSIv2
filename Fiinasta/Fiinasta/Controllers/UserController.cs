using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fiinasta.FiinastaAPI;
using System.Threading;

namespace Fiinasta.Controllers
{
    public class UserController : Controller
    {
        public ActionResult LoginTest()
        {
            Session["user"] = "user";
            return View("Index");
        }

        public ActionResult Logoff()
        {
            Session["user"] = null;
            Session["welcome"] = null;
            return View("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            if (ModelState.IsValid)
            {
                using(FiinastaAPIClient client = new FiinastaAPIClient())
                {
                    client.Register(user);                    
                }
                ModelState.Clear();
                ViewBag.Message = "Paskyra sėkmingai sukurta.";
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FiinastaAPIClient client = new FiinastaAPIClient();
                    var temp = client.Login(user);
                    Session["user"] = temp;
                    Session["welcome"] = temp.First_Name+" "+temp.Last_Name;
                    client.Close();
                }
                catch (ArgumentException e)
                {
                    ViewBag.Message = e.Message;                        
                }                
            }
            return View("Index");
        }
    }
}