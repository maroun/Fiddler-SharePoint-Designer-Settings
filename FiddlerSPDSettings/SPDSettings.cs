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
            this.miEnableSPD = new System.Windows.Forms.MenuItem(Constants.MenuItemSPD);
            this.miEnableRevertFromTemplate = new System.Windows.Forms.MenuItem(Constants.MenuItemRevertFromTemplate);
            this.miEnableMasterPageEditing = new System.Windows.Forms.MenuItem(Constants.MenuItemMasterPageEditing);
            this.miEnableUrlStructure = new System.Windows.Forms.MenuItem(Constants.MenuItemUrlStructure);

            this.miEnableSPD.Index = 0;
            this.miEnableRevertFromTemplate.Index = 1;
            this.miEnableMasterPageEditing.Index = 2;
            this.miEnableUrlStructure.Index = 3;

            this.mnuSPD = new System.Windows.Forms.MenuItem();
            this.mnuSPD.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.miEnableSPD, this.miEnableRevertFromTemplate, this.miEnableMasterPageEditing, this.miEnableUrlStructure });
            this.mnuSPD.Text = Constants.MenuBarName;

            this.miEnableSPD.Click += new System.EventHandler(this.miEnableSPD_Click);
            this.miEnableSPD.Checked = FiddlerApplication.Prefs.GetBoolPref(string.Format("{0}.{1}", Constants.Prefix, Constants.SPDEnabled), false);

            this.miEnableRevertFromTemplate.Click += new System.EventHandler(this.miEnableRevertFromTemplate_Click);
            this.miEnableRevertFromTemplate.Checked = FiddlerApplication.Prefs.GetBoolPref(string.Format("{0}.{1}", Constants.Prefix, Constants.RevertFromTemplateEnabled), false);

            this.miEnableMasterPageEditing.Click += new System.EventHandler(this.miEnableMasterPageEditing_Click);
            this.miEnableMasterPageEditing.Checked = FiddlerApplication.Prefs.GetBoolPref(string.Format("{0}.{1}", Constants.Prefix, Constants.MasterPageEditingEnabled), false);

            this.miEnableUrlStructure.Click += new System.EventHandler(this.miEnableUrlStructure_Click);
            this.miEnableUrlStructure.Checked = FiddlerApplication.Prefs.GetBoolPref(string.Format("{0}.{1}", Constants.Prefix, Constants.UrlStructureEnabled), false);
        }

        private static void invertCheckboxAndSave(MenuItem mi, string fiddlerSettingName)
        {
            mi.Checked = !mi.Checked;
            FiddlerApplication.Prefs.SetBoolPref(fiddlerSettingName, mi.Checked);
        }

        public SPDSettings()
        {
            this.initializeMenu();
        }

        public void miEnableUrlStructure_Click(object sender, EventArgs e)
        {
            invertCheckboxAndSave(this.miEnableUrlStructure, string.Format("{0}.{1}",Constants.Prefix,Constants.UrlStructureEnabled));
        }

        public void miEnableMasterPageEditing_Click(object sender, EventArgs e)
        {
            invertCheckboxAndSave(this.miEnableMasterPageEditing, string.Format("{0}.{1}", Constants.Prefix, Constants.MasterPageEditingEnabled));
        }

        public void miEnableRevertFromTemplate_Click(object sender, EventArgs e)
        {
            invertCheckboxAndSave(this.miEnableRevertFromTemplate, string.Format("{0}.{1}", Constants.Prefix, Constants.RevertFromTemplateEnabled));
        }

        public void miEnableSPD_Click(object sender, EventArgs e)
        {
            invertCheckboxAndSave(this.miEnableSPD, string.Format("{0}.{1}", Constants.Prefix, Constants.SPDEnabled));
        }

        public void OnPeekAtResponseHeaders(Session oSession) { }

        public void AutoTamperRequestAfter(Session oSession) { }

        public void AutoTamperRequestBefore(Session oSession) { }

        public void AutoTamperResponseAfter(Session oSession) { }

        public void AutoTamperResponseBefore(Session oSession)
        {
            if (this.miEnableSPD.Checked || this.miEnableRevertFromTemplate.Checked || this.miEnableMasterPageEditing.Checked || this.miEnableUrlStructure.Checked)
            {
                oSession.utilDecodeResponse();
            }

            if (this.miEnableSPD.Checked)
            {
                if (oSession.uriContains(Constants.AuthorDll) && oSession.oResponse.headers.ExistsAndContains(Constants.ContentType, Constants.ApplicationRpc))
                {
                    oSession.utilReplaceInResponse(Constants.DisableWebdesignFeatures, string.Empty);
                    oSession.utilReplaceInResponse(string.Format(Constants.AllowDesigner, "0"), string.Format(Constants.AllowDesigner, "1"));
                }
            }

            if (this.miEnableRevertFromTemplate.Checked)
            {
                if (oSession.uriContains(Constants.ProcessQuery) && oSession.oResponse.headers.ExistsAndContains(Constants.ContentType, Constants.ApplicationJson))
                {
                    oSession.utilReplaceInResponse(string.Format(Constants.AllowRevertFromTemplate, "false"), string.Format(Constants.AllowRevertFromTemplate, "true"));
                }
            }

            if (this.miEnableMasterPageEditing.Checked)
            {
                if (oSession.uriContains(Constants.ProcessQuery) && oSession.oResponse.headers.ExistsAndContains(Constants.ContentType, Constants.ApplicationJson))
                {
                    oSession.utilReplaceInResponse(string.Format(Constants.AllowMasterPageEditing, "false"), string.Format(Constants.AllowMasterPageEditing, "true"));
                }
            }

            if (this.miEnableUrlStructure.Checked)
            {
                if (oSession.uriContains(Constants.ProcessQuery) && oSession.oResponse.headers.ExistsAndContains(Constants.ContentType, Constants.ApplicationJson))
                {
                    oSession.utilReplaceInResponse(string.Format(Constants.ShowUrlStructure, "false"), string.Format(Constants.ShowUrlStructure, "true"));
                }

                if (oSession.uriContains(Constants.AuthorDll) && oSession.oResponse.headers.ExistsAndContains(Constants.ContentType, Constants.ApplicationRpc))
                {
                    oSession.utilReplaceInResponse(string.Format(Constants.ShowUrlStructureRpc, "0"), string.Format(Constants.ShowUrlStructureRpc, "1"));
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
