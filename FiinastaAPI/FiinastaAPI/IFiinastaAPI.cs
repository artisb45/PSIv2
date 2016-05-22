using FiinastaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FiinastaAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IFiinastaAPI
    {

        [OperationContract]
        void Register(Users user);
        [OperationContract]
        Users Login(Users user);
        [OperationContract]
        Contacts GetContacts();
        [OperationContract]
        bool CheckEntries(Users user);
        [OperationContract]
        void AddSpendings(Spendings spendings);
        [OperationContract]
        List<Spendings> GetSpendings(string period, Users user);
        [OperationContract]
        List<string> GetCategories();

        [OperationContract]
        List<Spendings> Search(string searchText, Users user);
        [OperationContract]
        SystemMessage GetMessageByID(string code);
    }
}
