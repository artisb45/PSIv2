using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FiinastaAPI.Models;

namespace FiinastaAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IFiinastaAPI
    {
        public void AddSpendings(Spendings spendings)
        {
            using (var db = new FiinastaDBEntities())
            {
                db.Spendings.Add(spendings);
                db.SaveChanges();
            }
        }

        public bool CheckEntries(Users user)
        {
            using (var db = new FiinastaDBEntities())
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
            using (var db = new FiinastaDBEntities())
            {
                var temp = db.Contacts.First();
                return temp;
            }
        }

        public List<Spendings> GetSpendings(string period, Users user)
        {
            var dates = period.Split('_');
            using (var db = new FiinastaDBEntities())
            {
                if (period != "")
                {
                    var d1 = DateTime.Parse(dates[0]);
                    var d2 = DateTime.Parse(dates[1]);
                    return
                        (from s in db.Spendings where s.Date >= d1 && s.Date <= d2 where s.UserID == user.ID select s)
                            .ToList();
                }
                return (from s in db.Spendings where s.UserID == user.ID select s).ToList();
            }
        }

        public Users Login(Users user)
        {
            using (var db = new FiinastaDBEntities())
            {
                try
                {
                    var temp = db.Users.First(u => u.E_Mail == user.E_Mail && u.Password == user.Password);
                    if (temp != null)
                    {
                        return temp;
                    }
                }
                catch(Exception e)
                {
                    throw new ArgumentException("Neteisingas el. paštas arba slaptažodis!");
                }
                return null;                     
            }
        }

        public void Register(Users user)
        {
            using (var db = new FiinastaDBEntities())
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<string> GetCategories()
        {
            using (var db = new FiinastaDBEntities())
            {
                return (from c in db.Category select c.Name).ToList();
            }
        }

        public List<Spendings> Search(string searchText, Users user)
        {
            using (var db = new FiinastaDBEntities())
            {
                var i = 0;
                var spendingsList = GetSpendings("", user);
                var sizeList = spendingsList.Count;
                var selectedList = new List<Spendings>();
                while (i < sizeList)
                {
                    if (Regex.IsMatch(spendingsList[i].Comment, searchText,
                        RegexOptions.IgnoreCase))
                    {
                        selectedList.Add(spendingsList[i]);
                    }
                    i++;
                }
                return selectedList;
            }
        }

        SystemMessage IFiinastaAPI.GetMessageByID(string code)
        {
            using (var db = new FiinastaDBEntities())
            {
                return db.SystemMessage.First(m => m.Code == code);
            }
        }
    }
}