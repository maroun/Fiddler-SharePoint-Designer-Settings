using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiddlerSPDSettings
{
    class Constants
    {
        #region Settings
        public const string Prefix = "extensions.spdsettings";

        public const string SPDEnabled = "SPDEnabled";
        public const string RevertFromTemplateEnabled = "RevertFromTemplateEnabled";
        public const string MasterPageEditingEnabled = "MasterPageEditingEnabled";
        public const string UrlStructureEnabled = "UrlStructureEnabled";
        #endregion

        #region Menu Elements
        public const string MenuBarName = "SharePoint Designer";
        public const string MenuItemSPD = "&Enable SharePoint Designer";
        public const string MenuItemRevertFromTemplate = "Enable &Detaching Pages from the Site Definition";
        public const string MenuItemMasterPageEditing = "Enable Customizing &Master Pages and Page Layouts";
        public const string MenuItemUrlStructure = "Enable Managing of the Web Site URL &Structure";
        #endregion

        #region Files
        public const string AuthorDll = "_vti_bin/_vti_aut/author.dll";
        public const string ProcessQuery = "_vti_bin/client.svc/ProcessQuery";
        #endregion

        #region Content Types
        public const string ContentType = "Content-Type";
        public const string ApplicationJson = "application/json";
        public const string ApplicationRpc = "application/x-vermeer-rpc";
        #endregion

        #region Content Strings
        public const string DisableWebdesignFeatures = "\n<li>vti_disablewebdesignfeatures2\n<li>SX|wdfopensite";
        public const string AllowDesigner = "\n<li>allowdesigner\n<li>SW|{0}";
        public const string AllowRevertFromTemplate = "\"AllowRevertFromTemplateForCurrentUser\":{0}";
        public const string AllowMasterPageEditing = "\"AllowMasterPageEditingForCurrentUser\":{0}";
        public const string ShowUrlStructure = "\"ShowUrlStructureForCurrentUser\":{0}";
        public const string ShowUrlStructureRpc = "\n<li>showurlstructure\n<li>SW|{0}";
        #endregion
    }
}
