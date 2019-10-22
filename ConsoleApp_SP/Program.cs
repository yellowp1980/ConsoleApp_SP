using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_SP.Services;
using ConsoleApp_SP.Models;
using Microsoft.SharePoint.Client;

namespace ConsoleApp_SP
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertItem insertItem = new InsertItem();
            //insertItem.insert();
            //Console.WriteLine("Insert Completed");
            //Console.ReadLine();

            //updateItem updateItem = new updateItem();
            //updateItem.update();
            //Console.WriteLine("Update Completed");
            //Console.ReadLine();

            var url = Common.GetAppSetting("SharePointLink");
            SPRepo _repo = new SPRepo(url);
            var _ctx = _repo.GetSPClientContext(url);
            List list = _repo.GetList("Individual");


            ///////
            //List table = _ctx.Web.Lists.GetByTitle("Individual");
            //CamlQuery cqObj = CamlQuery.CreateAllItemsQuery(100);
            //ListItemCollection collOjb = table.GetItems(cqObj);

            //_ctx.Load(collOjb);
            //_ctx.ExecuteQuery();
            //if (collOjb.Count != 0)
            //{
            //    foreach (ListItem lt in collOjb)
            //    {
            //        Console.WriteLine(lt["Age"]);
            //    }
            //}


            IndividualItem ind = new IndividualItem(){ Title = "Mr", FirstName = "Ping", LastName = "Huang", Age = "38", Gender = "Male" };         
            bool flag = _repo.insertRecord(_ctx, "Individual", ind);
            //bool flag = _repo.insertRecord(_ctx, list, ind);

            //Console.Write("Please enter field Name: ");
            //string fieldName = Console.ReadLine();
            //Console.Write("Please enter field Value: ");
            //string fieldValue = Console.ReadLine();

            //Console.Write("Please enter target field Name: ");
            //string tfieldName = Console.ReadLine();
            //Console.Write("Please enter target field Value: ");
            //string tfieldValue = Console.ReadLine();

            //bool flag = _dr.updateRecord_simple(_ctx, "Individual", fieldName, fieldValue, tfieldName, tfieldValue);

            if (flag)
                Console.WriteLine("Action completed");
            Console.ReadLine();
        }
    }
}
