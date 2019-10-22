using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security;
using System.Threading;
using System.Globalization;
using ConsoleApp_SP.Models;

namespace ConsoleApp_SP.Services
{
    class SPRepo
    {
        private ClientContext _ctx;

        public SPRepo(string url)
        {
            _ctx = GetSPClientContext(url);
        }

        public ClientContext GetSPClientContext(string url)
        {
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var ctx = new ClientContext(url);

            string username = Common.GetAppSetting("Username");
            string password = Common.GetAppSetting("Password");

            //SecureString password = new SecureString();
            //foreach (char c in Common.GetAppSetting("Password"))
            //    password.AppendChar(c);

            ctx.Credentials = CredentialsFactory.SharepointCredentials.CreateCredentials(username, password,CredentialsFactory.SharePointAuthentication.SharePointOnline);
            
            return ctx;
        }

        public List GetList(string listTitle)
        {
            List list = _ctx.Web.Lists.GetByTitle(listTitle);
            _ctx.Load(list);
            _ctx.ExecuteQuery();

            return list;
        }

        public bool updateRecord_simple(ClientContext _ctx, List oList, string fieldName, string fieldValue, string targetField, string targetValue)
        {
            try
            {

                CamlQuery query = new CamlQuery
                {
                    ViewXml = string.Format("<View><Query><Where><Eq><FieldRef Name='{0}'/><Value Type='text'>{1}</Value></Eq></Where></Query></View>", fieldName, fieldValue)
                };

                ListItemCollection itemsPH = oList.GetItems(query);
                _ctx.Load(itemsPH);
                _ctx.ExecuteQuery();

                foreach (ListItem oListItem in itemsPH)
                {
                    oListItem[targetField] = targetValue;
                    oListItem.Update();
                    _ctx.ExecuteQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        //public bool insertRecord(ClientContext _ctx, List oList, IndividualItem ind)
        public bool insertRecord(ClientContext _ctx, string LastName, IndividualItem ind)
        {
            try
            {
                List oList = _ctx.Web.Lists.GetByTitle(LastName);
                ListItemCreationInformation listCreationInformation = new ListItemCreationInformation();
                ListItem oListItem = oList.AddItem(listCreationInformation);

                oListItem["Title"] = ind.Title;
                oListItem["FirstName"] = ind.FirstName;
                oListItem["LastName"] = ind.LastName;
                oListItem["Age"] = ind.Age;
                oListItem["Gender"] = ind.Gender;

                oListItem.Update();
                _ctx.ExecuteQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }//ends class
}
