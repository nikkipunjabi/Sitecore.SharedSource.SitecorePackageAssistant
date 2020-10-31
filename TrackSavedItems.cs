using Sitecore.Configuration;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.SharedSource.SitecorePackageAssistant
{
    public class TrackSavedItems
    {
        public string Database { get; private set; }

        public void OnItemSaved(object sender, EventArgs args)
        {
            this.Database = "master";

            Sitecore.Events.SitecoreEventArgs eventArgs = args as Sitecore.Events.SitecoreEventArgs;
            Sitecore.Diagnostics.Assert.IsNotNull(eventArgs, "eventArgs");
            Sitecore.Data.Items.Item item = eventArgs.Parameters[0] as Sitecore.Data.Items.Item;
            Sitecore.Diagnostics.Assert.IsNotNull(item, "item");

            //{22219A9A-27BB-4506-B8C8-70DDA639C7F2} -- is the id of Sitecore PowerShell Extensions admin item
            //sitecore/system/Modules/PowerShell/Settings/ISE/sitecore/admin
            //It autosave the script when we execute. 
            //This item shouldn't be tracked at all.
            //It can be added in untracked item as well

            if (item.Database != null && item.ID != new Sitecore.Data.ID("{BECB151A-562F-4D0C-87A7-A3CBAC3220D9}")
                && item.ID != new Data.ID("{22219A9A-27BB-4506-B8C8-70DDA639C7F2}"))
            {
                try
                {
                    var trackingItem = Factory.GetDatabase("master").GetItem("{BECB151A-562F-4D0C-87A7-A3CBAC3220D9}");
                    var newid = item.ID.ToString();
                    bool doTrackItem = true;

                    //Do not track if this ID is present in Untrack Items
                    var untrackItemsList = trackingItem["Untrack Items"];
                    if (untrackItemsList != null && !string.IsNullOrWhiteSpace(untrackItemsList) && untrackItemsList.Contains(newid))
                    {
                        doTrackItem = false;
                    }
                    if (doTrackItem)
                    {
                        var currentItemsList = trackingItem["Current Saved Items"];
                        if (string.IsNullOrWhiteSpace(currentItemsList))
                        {
                            trackingItem.Editing.BeginEdit();
                            trackingItem["Current Saved Items"] = newid;
                            trackingItem.Editing.EndEdit();

                        }
                        else
                        {
                            var currentString = trackingItem["Current Saved Items"];
                            if (currentString.Contains(newid))
                            {
                                //Do Nothing
                            }
                            else
                            {
                                trackingItem.Editing.BeginEdit();
                                trackingItem["Current Saved Items"] = currentString + "\r\n" + newid;
                                trackingItem.Editing.EndEdit();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //throw ex;
                    //Sitecore.Diagnostics.Log("Sitecore Package Assistant. Error: " + ex.StackTrace);                    
                }
                finally
                {

                }
            }
        }

        public void OnItemCreated(object sender, EventArgs args)
        {
            var createdArgs = Event.ExtractParameter(args, 0) as ItemCreatedEventArgs;

            Assert.IsNotNull(createdArgs, "args");
            if (createdArgs != null)
            {
                Assert.IsNotNull(createdArgs.Item, "item");
                if (createdArgs.Item != null)
                {
                    Item item = createdArgs.Item;
                    var trackingItem = Factory.GetDatabase("master").GetItem("{BECB151A-562F-4D0C-87A7-A3CBAC3220D9}");
                    var currentItemsList = trackingItem["Current Saved Items"];
                    if (string.IsNullOrWhiteSpace(currentItemsList))
                    {
                        trackingItem.Editing.BeginEdit();
                        trackingItem["Current Saved Items"] = item.ID.ToString();
                        trackingItem.Editing.EndEdit();

                    }
                    else
                    {
                        var currentString = trackingItem["Current Saved Items"];
                        if (currentString.Contains(item.ID.ToString()))
                        {
                            //Do Nothing
                        }
                        else
                        {
                            var newid = "\r\n" + item.ID.ToString();
                            trackingItem.Editing.BeginEdit();
                            trackingItem["Current Saved Items"] = currentString + newid;
                            trackingItem.Editing.EndEdit();

                        }
                    }
                }
            }
        }
    }
}
