using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Configuration;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.SitecorePackageCreator
{
    public class UntrackItemCommand : Sitecore.Shell.Framework.Commands.Command
    {
        public override void Execute(Sitecore.Shell.Framework.Commands.CommandContext context)
        {
            if (context.Items != null && context.Items.Length > 0)
            {
                Item contextItem = context.Items[0];
                NameValueCollection parameters = new NameValueCollection();
                parameters["id"] = contextItem.ID.ToString();
                parameters["name"] = contextItem.Name;
                parameters["database"] = contextItem.Database.Name;
                parameters["language"] = contextItem.Language.ToString();
                Sitecore.Context.ClientPage.Start(this, "Run", parameters);//Run method executes on

            }
        }

        protected void Run(Sitecore.Web.UI.Sheer.ClientPipelineArgs args)
        {
            if (args.IsPostBack)
            {
                if (!(String.IsNullOrEmpty(args.Result)) && (args.Result != "undefined") && (args.Result != "null"))
                {
                }
            }
            else
            {
                //Sitecore.Text.UrlString url = new Sitecore.Text.UrlString("/sitecore modules/shell/Demo/News.aspx");
                //url.Append("id", args.Parameters["id"]);
                //url.Append("database", args.Parameters["database"]);

                var trackingItem = Factory.GetDatabase("master").GetItem("{BECB151A-562F-4D0C-87A7-A3CBAC3220D9}");
                var newID = args.Parameters["id"];
                var currentItemsList = trackingItem["Untrack Items"];
                if (string.IsNullOrWhiteSpace(currentItemsList))
                {
                    trackingItem.Editing.BeginEdit();
                    trackingItem["Untrack Items"] = args.Parameters["id"];
                    trackingItem.Editing.EndEdit();

                }
                else
                {
                    if (currentItemsList.Contains(newID))
                    {
                        //Do Nothing
                    }
                    else
                    {
                        trackingItem.Editing.BeginEdit();
                        trackingItem["Untrack Items"] = currentItemsList + "\r\n" + newID;
                        trackingItem.Editing.EndEdit();

                    }
                }

                //Remove Item ID from Current Saved Items, if present.
                var currentTrackedItems = trackingItem["Current Saved Items"];
                if (currentTrackedItems.Contains(newID))
                {
                    currentTrackedItems = currentTrackedItems.Replace("\r\n" + newID, "");
                    currentTrackedItems = currentTrackedItems.Replace(newID, "");
                    trackingItem.Editing.BeginEdit();
                    trackingItem["Current Saved Items"] = currentTrackedItems;
                    trackingItem.Editing.EndEdit();
                }

                args.WaitForPostBack(true);
            }
        }
    }
}
