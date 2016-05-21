using Fiinasta.FiinastaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiinasta.Controllers
{
    public class ContactsController : Controller
    {
        // GET: Contacts
        public ActionResult Run()
        {
            using (FiinastaAPIClient client = new FiinastaAPIClient())
            {
                return View("Contacts",client.GetContacts());
            }  
        }
    }
}