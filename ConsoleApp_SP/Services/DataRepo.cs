using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_SP.Services;
using ConsoleApp_SP.Models;

namespace ConsoleApp_SP.Services
{
    public class DataRepo
    {

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
