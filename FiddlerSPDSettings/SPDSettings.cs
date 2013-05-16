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
        private bool enableSpd = false;
        private bool enableUrlStructure = false;
        private bool enableRevertFromTemplate = false;
        private bool enableMasterPageEditing = false;
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
            this.miEnableSPD.Checked = enableSpd;

            this.miEnableRevertFromTemplate.Click += new System.EventHandler(this.miEnableRevertFromTemplate_Click);
            this.miEnableRevertFromTemplate.Checked = enableRevertFromTemplate;

            this.miEnableMasterPageEditing.Click += new System.EventHandler(this.miEnableMasterPageEditing_Click);
            this.miEnableMasterPageEditing.Checked = enableMasterPageEditing;

            this.miEnableUrlStructure.Click += new System.EventHandler(this.miEnableFileListing_Click);
            this.miEnableUrlStructure.Checked = enableUrlStructure;
        }

        public void miEnableFileListing_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void miEnableMasterPageEditing_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void miEnableRevertFromTemplate_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void miEnableSPD_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnPeekAtResponseHeaders(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void AutoTamperRequestAfter(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void AutoTamperRequestBefore(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void AutoTamperResponseAfter(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void AutoTamperResponseBefore(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void OnBeforeReturningError(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void OnBeforeUnload()
        {
            throw new NotImplementedException();
        }

        public void OnLoad()
        {
            FiddlerApplication.UI.mnuMain.MenuItems.Add(mnuSPD);
        }
    }
}
