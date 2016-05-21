using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FiinastaAPI.Models;
using System.ServiceModel.Description;
using System.Threading;

namespace FiinastaAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IFiinastaAPI
    {
        public void AddSpendings(Spendings spendings)
        {
            using (FiinastaDBEntities db = new FiinastaDBEntities())
            {
                db.Spendings.Add(spendings);
                db.SaveChanges();
            }
        }

        public bool CheckEntries(Users user)
        {
            using (FiinastaDBEntities db = new FiinastaDBEntities())
            {
                if ((from s in db.Spendings where s.UserID == user.ID select s).Count() != 0)
                {
                    return true;
                }
                return false;
            }
        }

        public Contacts GetContacts()
        {
            using (FiinastaDBEntities db = new FiinastaDBEntities())
            {
                var temp = db.Contacts.First();
                return temp;
            }
        }

        public List<Spendings> GetSpendings(string period)
        {
            var dates = period.Split('-');
            using (FiinastaDBEntities db = new FiinastaDBEntities())
            {
                return (from s in db.Spendings where s.Date >= DateTime.Parse(dates[0]) && s.Date <= DateTime.Parse(dates[1]) select s).ToList();
            }
        }

        public Users Login(Users user)
        {
            using(FiinastaDBEntities db = new FiinastaDBEntities())
            {
                var temp = db.Users.First((u) => u.E_Mail == user.E_Mail && u.Password == user.Password);
                if(temp != null)
                {                  
                    return temp;
                }
                else
                {
                    throw new ArgumentException("Neteisingas el. paštas arba slaptažodis!");
                }
            }
        }

        public void Register(Users user)
        {
            using(FiinastaDBEntities db = new FiinastaDBEntities())
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    throw e;
                }                
            }
        }
    }
}
