------------------------
 DNNspot Store Module
------------------------
 Powerful, yet simple eCommerce module for DotNetNuke.

----------------------------
 Hosting/Site Requirements:
----------------------------    
 *   ASP.NET 2.0
 *   .NET 3.5 Framework
 *   DotNetNuke 05.03.01+
    

----------------------------
 Get Started - Quick Guide
----------------------------
1.  Install the correct ZIP package for your DNN version
        1a. See "special instructions" below if you're installing on DNN 4.9.x !!

2.  Make sure you did not receive any error messages during the module install process.
        2a. If you received errors during installation, please note the full error message
            and report it to us at: www.DNNspot.com

3.  Create 3 new pages on your DNN site:
    - Store
    - My Orders
    - Store Admin
    
4.  Go to your "Store" page and add the "DNNspot Store" module to the page
    You should see 3 new store modules added to the page:
        - MainDispatch  (This will show the product list, product detail, and cart/checkout screens)
        - CategoryMenu  (Displays categories for browsing products, usually placed in a left/right pane)
        - MiniCart      (Mini-display of the user's cart: numer of items and the total amount)
    
    You may rearrange these modules however you like, move them to different panes, etc.
    
5.  Go to your "My Orders" page and add the "DNNSpot Store - My Orders" module to the page
    This module allows users to search their order history and find past orders
    
6.  Go to your "Store Admin" page and add the "DNNspot Store - Admin" module to the page
        6a. Add a few product categories by clicking on the "Categories" link
        6b. Add a few products by clicking on the "Products" link
            When adding products, make sure to put your products into at least 1 category,
            otherwise they will not be displayed!
            
7.  Go back to the "Store" page, browse through your products and check-out!
    That's all for the quick guide, but you can configure many more store options
    in the "Store Admin" area. Configure shipping, tax, emails, etc.

-------------------------------------------- 
 SPECIAL INSTRUCTIONS for DotNetNuke 4.9.x
--------------------------------------------
 This module requires changes to the web.config file
 in order to support new .NET 3.5 features for DNN 4.x.
 
 The easiest way to make these changes is to FIRST install the
 included "DNN4-LinqPrep_01.00.00_Install.zip" module on your DNN 4.x site,
 add it to a page, and click on the "Update web.config" button.
 Check here for more help with this process: http://adefwebserver.com/dotNetNukeHELP/Misc/LinqPrep/
 
 After the "LinqPrep" module has been installed and you have updated the web.config,
 you can proceed to install the DNNspot-Store ZIP Package.
 
 
 -------------------------------------------
 SPECIAL INSTRUCTIONS for iFinity 
 
 FriendlyUrl Provider / iFinity URL Master
--------------------------------------------
 If iFinity is configured with redirectUnfriendly="false", then no changes are necessary.
 
 However, if you have iFinity configured with redirectUnfriendly="true"
 then you MUST UPDATE the "iFinity.FriendlyUrl" section of your web.config.
 
 How to update your web.config to make the Store compatible with iFinity FriendlyUrl / Url Master:
 
    1) Make a note of the names of any pages that have the Store category, product, or cart/checkout modules on them (e.g. "Store"). 
        The "Store Admin" module is not affected by iFinity, only the other modules.
        
    2) Update the "iFinity.FriendlyUrl" section of your web.config and add these page names to the "doNotRedirectRegex" attribute.
        You should add each page name, followed by ".*"
        For example, with a page named "Store" the attribute would be: doNotRedirectRegex="Store.*"
        If you have store modules on multiple pages the attribute would be: doNotRedirectRegex="Store.*|My-Other-Page.*|A-third-page.*"
