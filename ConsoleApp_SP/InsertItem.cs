using System;
using Microsoft.SharePoint.Client;
using System.Security;
using System.Threading;
using System.Globalization;
using System.Configuration;
using ConsoleApp_SP.Services;

namespace ConsoleApp_SP
{
    public class InsertItem
    {
        public void insert()
        {
            try
            {
                SecureString Password = new SecureString();

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                foreach (char c in Common.GetAppSetting("Password"))
                    Password.AppendChar(c);

                ClientContext clientContext = new ClientContext(Common.GetAppSetting("SharePointLink"));
                clientContext.Credentials = new SharePointOnlineCredentials(Common.GetAppSetting("Username"), Password);

                List oList = clientContext.Web.Lists.GetByTitle(Common.GetAppSetting("SharePointListName"));

                ListItemCreationInformation listCreationInformation = new ListItemCreationInformation();
                ListItem oListItem = oList.AddItem(listCreationInformation);

                oListItem["Title"] = "Mr";
                oListItem["FirstName"] = "James";
                oListItem["LastName"] = "Wang";
                oListItem["Age"] = "11";
                oListItem["Gender"] = "Male";

                oListItem.Update();
                clientContext.ExecuteQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
