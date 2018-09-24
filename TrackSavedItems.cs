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

namespace Sitecore.SharedSource.SitecorePackageCreator
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
            if (item.Database != null && item.ID != new Sitecore.Data.ID("{BECB151A-562F-4D0C-87A7-A3CBAC3220D9}"))
            {
                try
                {
                    var trackingItem = Factory.GetDatabase("master").GetItem("{BECB151A-562F-4D0C-87A7-A3CBAC3220D9}");
                    var newid = "\r\n" + item.ID.ToString();
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
                            trackingItem["Current Saved Items"] = item.ID.ToString();
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
                                trackingItem["Current Saved Items"] = currentString + newid;
                                trackingItem.Editing.EndEdit();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
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
                        var newid = "\r\n" + item.ID.ToString();
                        if (currentString.Contains(newid))
                        {
                            //Do Nothing
                        }
                        else
                        {
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
