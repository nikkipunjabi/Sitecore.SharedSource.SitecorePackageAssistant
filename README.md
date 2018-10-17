# Sitecore Package Assistant:

![](https://isaadansari.files.wordpress.com/2018/10/packages.png)

Sitecore Package Assistant reduces the burden of package creation. It has the following features:

Sitecore Package Assistant is composed of two parts, here is the Short Overview of Logic

### 1. Sitecore SharedSource Code

![](https://isaadansari.files.wordpress.com/2018/10/power-module.png)

Sitecore SharedSource Code has the complete logic of the managing items tracked and which items to be excluded from the package, and the packages history.

### 2. Sitecore Powershell Extension

![](https://isaadansari.files.wordpress.com/2018/10/power-shell-extension.png)

The package creation is being done using Sitecore Powershell Extensions as base module and running Powershell Scripts for creating packages.

It uses Sitecore Gutter to show the marked items Ready for Sitecore Package, and which items to be excluded.


# How it works:

### 1. Prerequisite:

Install the prerequisite, Sitecore Powershell Extensions. using the Sitecore installation wizard.

### 2. Install the Sitecore Package Assistant:

Install the Sitecore Package Assistant, in case you don’t see (add-icon). This icon at the very left upon saving you item. Go to
/sitecore/system/Modules/PowerShell/Script Library/Sitecore Package Assistant/Content Editor/Context Menu/Sitecore Package Assistant/Create Package

And Right-Click to open it with SPE, Click on the “Edit with ISE”, the popup opens up, go to Settings Tab, Click Rebuild All, and Click on the Sync Library with the Content Editor Gutter.

![](https://isaadansari.files.wordpress.com/2018/10/2018-10-02_2232.png)

### 3. Automatically marking the saved/changed items

Sitecore Package Assistant work on his own. What you need to do is just nothing and keep on working with the Sitecore Solution. It will automatically mark the items you have worked on with the  add package icon  
It also tracks the newly created items and make them available for the new Package.

![](https://isaadansari.files.wordpress.com/2018/10/item-sitecore-package-assistant-add.png)

For the items you had worked on marked with , or made changes, the IDs of those items will be saved in the item `SPA : {BECB151A-562F-4D0C-87A7-A3CBAC3220D9}` placed at `/sitecore/system/Modules/Sitecore Package Assistant/SPA`

![](https://isaadansari.files.wordpress.com/2018/10/2018-10-02_15581.png?w=739)

### 4. Excluding the Unwanted Items from Package

For all those items unnecessary for the release or any environment, you want to permanently exclude the items until or unless added back to to tracked items, you click on the tracked items at the very left in the Sitecore Gutter, which marks the item with remove package icon 

![](https://isaadansari.files.wordpress.com/2018/10/item-sitecore-package-assistant-delete.png)

Similar to adding the Curent Saved Items, the IDs for the excluded items marked with  are added to the Untrack Items in the item `SPA : {BECB151A-562F-4D0C-87A7-A3CBAC3220D9}` placed at `/sitecore/system/Modules/Sitecore Package Assistant/SPA`

![](https://isaadansari.files.wordpress.com/2018/10/2018-10-02_16001.png)

### 5. Maintaining the history for the packages

For all those items marked with  are ready to be packaged. You can Right-Click on the Context Menu and go to Scripts > Sitecore Package Assistant > Create Package And click the Create Package 

![](https://isaadansari.files.wordpress.com/2018/10/2018-10-02_2215.png)

And your package will be available here.

![](https://isaadansari.files.wordpress.com/2018/10/2018-10-02_22181.png)

And once you create package, the history of packages will be maintained here.

![](https://isaadansari.files.wordpress.com/2018/10/2018-10-02_2218-2.jpg)

The incremental packages in which the items are dependent on one another, will be easily tracked. And if missed you can easily follow the history and install the packages one by one, and get it sorted.
This will also reduce the failed packages in stallions errors. As installation packages either small ones, take much time than expected ed, you might have noticed it while installing Sitecore Powershell Extensions. And for the larger packages of enterprise level, where there is much content, I can understand the pain it causes.

# Troubleshooting

###  Enabling Sitecore Package Assistant 

If you don't see this, make sure, you have the Sitecore Package Assistant enabled at the Content Menu. Just Right-Click at the very left at the Gutter and check if the Sitecore Package Assistant is enabled.

![](https://isaadansari.files.wordpress.com/2018/10/2018-10-02_2232.png)

### Icons does not show upon saving at Sitecore Gutter

Install the Sitecore Package Assistant,  in case you don't see   this icon at the very left upon saving you item. Go to
`/sitecore/system/Modules/PowerShell/Script Library/Sitecore Package Assistant/Content Editor/Context Menu/Sitecore Package Assistant/Create Package`

And Right-Click to open it with SPE, Click on the *"Edit with ISE"*, the popup opens up, go to Settings Tab, Click Rebuild All, and Click on the Sync Library with the Content Editor Gutter.

![](https://isaadansari.files.wordpress.com/2018/10/2018-10-02_1655.png)


# Reference Links

[Sitecore Package Assistant](https://marketplace.sitecore.net/Modules/S/Sitecore_Package_Assistant.aspx)

[Sitecore Experience Platform 9 update 1](https://dev.sitecore.net/Downloads.aspx)

[Sitecore Powershell Extensions](https://marketplace.sitecore.net/en/Modules/Sitecore_PowerShell_console.aspx)


 

