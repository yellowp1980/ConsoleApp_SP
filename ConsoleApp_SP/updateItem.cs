using System;
using Microsoft.SharePoint.Client;
using System.Security;
using System.Threading;
using System.Globalization;
using System.Configuration;
using ConsoleApp_SP.Services;


namespace ConsoleApp_SP
{
    public class updateItem
    {
        public void update()
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


                CamlQuery query = new CamlQuery {
                    ViewXml = string.Format("<View><Query><Where><Eq><FieldRef Name='FirstName'/><Value Type='text'>{0}</Value></Eq></Where></Query></View>","test")
                };

                ListItemCollection itemsPH = oList.GetItems(query);
                clientContext.Load(itemsPH);
                clientContext.ExecuteQuery();

                //foreach (ListItem oListItem in itemsPH)
                //{
                //    oListItem["LastName"] = "Yang";
                //    oListItem.Update();
                //    clientContext.ExecuteQuery();
                //}


                ////update single record
                ListItem li = itemsPH[0];
                li["LastName"] = "Huang1";

                li.Update();
                clientContext.ExecuteQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
