using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace FiddlerSPDSettings
{
    public class SPDSettings : IAutoTamper2
    {
        private MenuItem miEnableSPD;
        private MenuItem miEnableUrlStructure;
        private MenuItem miEnableRevertFromTemplate;
        private MenuItem miEnableMasterPageEditing;
        private MenuItem mnuSPD;

        private void initializeMenu()
        {
            this.miEnableSPD = new System.Windows.Forms.MenuItem("&Enable SharePoint Designer");
            this.miEnableRevertFromTemplate = new System.Windows.Forms.MenuItem("Enable &Detaching Pages from the Site Definition");
            this.miEnableMasterPageEditing = new System.Windows.Forms.MenuItem("Enable Customizing &Master Pages and Page Layouts ");
            this.miEnableUrlStructure = new System.Windows.Forms.MenuItem("Enable Managing of the Web Site URL &Structure ");

            this.miEnableSPD.Index = 0;
            this.miEnableRevertFromTemplate.Index = 1;
            this.miEnableMasterPageEditing.Index = 2;
            this.miEnableUrlStructure.Index = 3;

            this.mnuSPD = new System.Windows.Forms.MenuItem();
            this.mnuSPD.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.miEnableSPD, this.miEnableRevertFromTemplate, this.miEnableMasterPageEditing, this.miEnableUrlStructure });
            this.mnuSPD.Text = "SharePoint Designer";

            this.miEnableSPD.Click += new System.EventHandler(this.miEnableSPD_Click);
            this.miEnableSPD.Checked = FiddlerApplication.Prefs.GetBoolPref("extensions.autoenablespd.enableSpd", false);

            this.miEnableRevertFromTemplate.Click += new System.EventHandler(this.miEnableRevertFromTemplate_Click);
            this.miEnableRevertFromTemplate.Checked = FiddlerApplication.Prefs.GetBoolPref("extensions.autoenablespd.enableRevertFromTemplate", false);

            this.miEnableMasterPageEditing.Click += new System.EventHandler(this.miEnableMasterPageEditing_Click);
            this.miEnableMasterPageEditing.Checked = FiddlerApplication.Prefs.GetBoolPref("extensions.autoenablespd.enableMasterPageEditing", false);

            this.miEnableUrlStructure.Click += new System.EventHandler(this.miEnableUrlStructure_Click);
            this.miEnableUrlStructure.Checked = FiddlerApplication.Prefs.GetBoolPref("extensions.autoenablespd.enableUrlStructure", false);
        }

        public SPDSettings()
        {
            this.initializeMenu();
        }

        public void miEnableUrlStructure_Click(object sender, EventArgs e)
        {
            miEnableUrlStructure.Checked = !miEnableUrlStructure.Checked;
            FiddlerApplication.Prefs.SetBoolPref("extensions.autoenablespd.enableFileListing", miEnableUrlStructure.Checked);
        }

        public void miEnableMasterPageEditing_Click(object sender, EventArgs e)
        {
            miEnableMasterPageEditing.Checked = !miEnableMasterPageEditing.Checked;
            FiddlerApplication.Prefs.SetBoolPref("extensions.autoenablespd.enableMasterPageEditing", miEnableMasterPageEditing.Checked);
        }

        public void miEnableRevertFromTemplate_Click(object sender, EventArgs e)
        {
            miEnableRevertFromTemplate.Checked = !miEnableRevertFromTemplate.Checked;
            FiddlerApplication.Prefs.SetBoolPref("extensions.autoenablespd.enableRevertFromTemplate", miEnableRevertFromTemplate.Checked);
        }

        public void miEnableSPD_Click(object sender, EventArgs e)
        {
            miEnableSPD.Checked = !miEnableSPD.Checked;
            FiddlerApplication.Prefs.SetBoolPref("extensions.autoenablespd.enableSpd", miEnableSPD.Checked);
        }

        public void OnPeekAtResponseHeaders(Session oSession) { }

        public void AutoTamperRequestAfter(Session oSession) { }

        public void AutoTamperRequestBefore(Session oSession) { }

        public void AutoTamperResponseAfter(Session oSession) { }

        public void AutoTamperResponseBefore(Session oSession)
        {
            if (this.miEnableSPD.Checked)
            {
                if (oSession.uriContains("_vti_bin/_vti_aut/author.dll") && oSession.oResponse.headers.ExistsAndContains("Content-Type", "application/x-vermeer-rpc"))
                {
                    oSession.utilDecodeResponse();
                    oSession.utilReplaceInResponse("\n<li>vti_disablewebdesignfeatures2\n<li>SX|wdfopensite", "");
                    oSession.utilReplaceInResponse("\n<li>showurlstructure\n<li>SW|0", "\n<li>showurlstructure\n<li>SW|1");
                    oSession.utilReplaceInResponse("\n<li>allowdesigner\n<li>SW|0", "\n<li>allowdesigner\n<li>SW|1");
                }
            }

            if (this.miEnableRevertFromTemplate.Checked)
            {
                if (oSession.uriContains("_vti_bin/client.svc/ProcessQuery") && oSession.oResponse.headers.ExistsAndContains("Content-Type", "application/json"))
                {
                    oSession.utilDecodeResponse();
                    oSession.utilReplaceInResponse("\"AllowRevertFromTemplateForCurrentUser\":false", "\"AllowRevertFromTemplateForCurrentUser\":true");
                }
            }

            if (this.miEnableMasterPageEditing.Checked)
            {
                if (oSession.uriContains("_vti_bin/client.svc/ProcessQuery") && oSession.oResponse.headers.ExistsAndContains("Content-Type", "application/json"))
                {
                    oSession.utilDecodeResponse();
                    oSession.utilReplaceInResponse("\"AllowMasterPageEditingForCurrentUser\":false", "\"AllowMasterPageEditingForCurrentUser\":true");
                }
            }

            if (this.miEnableUrlStructure.Checked)
            {
                if (oSession.uriContains("_vti_bin/client.svc/ProcessQuery") && oSession.oResponse.headers.ExistsAndContains("Content-Type", "application/json"))
                {
                    oSession.utilDecodeResponse();
                    oSession.utilReplaceInResponse("\"ShowUrlStructureForCurrentUser\":false", "\"ShowUrlStructureForCurrentUser\":true");
                }
            }
        }

        public void OnBeforeReturningError(Session oSession) {}

        public void OnBeforeUnload() {}

        public void OnLoad()
        {
            FiddlerApplication.UI.mnuMain.MenuItems.Add(mnuSPD);
        }
    }
}
