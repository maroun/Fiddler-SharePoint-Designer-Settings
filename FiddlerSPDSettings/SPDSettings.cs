using Fiddler;
using System;
using System.Windows.Forms;
namespace FiddlerSPDSettings
{
    public class SPDSettings : IAutoTamper2
    {
        private MenuItem _miEnableSpd;
        private MenuItem _miEnableUrlStructure;
        private MenuItem _miEnableRevertFromTemplate;
        private MenuItem _miEnableMasterPageEditing;
        private MenuItem _mnuSpd;

        private void InitializeMenu()
        {
            _miEnableSpd = new MenuItem(Constants.MenuItemSPD);
            _miEnableRevertFromTemplate = new MenuItem(Constants.MenuItemRevertFromTemplate);
            _miEnableMasterPageEditing = new MenuItem(Constants.MenuItemMasterPageEditing);
            _miEnableUrlStructure = new MenuItem(Constants.MenuItemUrlStructure);

            _miEnableSpd.Index = 0;
            _miEnableRevertFromTemplate.Index = 1;
            _miEnableMasterPageEditing.Index = 2;
            _miEnableUrlStructure.Index = 3;

            _mnuSpd = new MenuItem();
            _mnuSpd.MenuItems.AddRange(new[] { _miEnableSpd, _miEnableRevertFromTemplate, _miEnableMasterPageEditing, _miEnableUrlStructure });
            _mnuSpd.Text = Constants.MenuBarName;

            _miEnableSpd.Click += miEnableSPD_Click;
            _miEnableSpd.Checked = FiddlerApplication.Prefs.GetBoolPref(string.Format("{0}.{1}", Constants.Prefix, Constants.SPDEnabled), false);

            _miEnableRevertFromTemplate.Click += miEnableRevertFromTemplate_Click;
            _miEnableRevertFromTemplate.Checked = FiddlerApplication.Prefs.GetBoolPref(string.Format("{0}.{1}", Constants.Prefix, Constants.RevertFromTemplateEnabled), false);

            _miEnableMasterPageEditing.Click += miEnableMasterPageEditing_Click;
            _miEnableMasterPageEditing.Checked = FiddlerApplication.Prefs.GetBoolPref(string.Format("{0}.{1}", Constants.Prefix, Constants.MasterPageEditingEnabled), false);

            _miEnableUrlStructure.Click += miEnableUrlStructure_Click;
            _miEnableUrlStructure.Checked = FiddlerApplication.Prefs.GetBoolPref(string.Format("{0}.{1}", Constants.Prefix, Constants.UrlStructureEnabled), false);
        }

        private static void InvertCheckboxAndSave(MenuItem mi, string fiddlerSettingName)
        {
            mi.Checked = !mi.Checked;
            FiddlerApplication.Prefs.SetBoolPref(fiddlerSettingName, mi.Checked);
        }

        public SPDSettings()
        {
            InitializeMenu();
        }

        public void miEnableUrlStructure_Click(object sender, EventArgs e)
        {
            InvertCheckboxAndSave(_miEnableUrlStructure, string.Format("{0}.{1}",Constants.Prefix,Constants.UrlStructureEnabled));
        }

        public void miEnableMasterPageEditing_Click(object sender, EventArgs e)
        {
            InvertCheckboxAndSave(_miEnableMasterPageEditing, string.Format("{0}.{1}", Constants.Prefix, Constants.MasterPageEditingEnabled));
        }

        public void miEnableRevertFromTemplate_Click(object sender, EventArgs e)
        {
            InvertCheckboxAndSave(_miEnableRevertFromTemplate, string.Format("{0}.{1}", Constants.Prefix, Constants.RevertFromTemplateEnabled));
        }

        public void miEnableSPD_Click(object sender, EventArgs e)
        {
            InvertCheckboxAndSave(_miEnableSpd, string.Format("{0}.{1}", Constants.Prefix, Constants.SPDEnabled));
        }

        public void OnPeekAtResponseHeaders(Session oSession) { }

        public void AutoTamperRequestAfter(Session oSession) { }

        public void AutoTamperRequestBefore(Session oSession) { }

        public void AutoTamperResponseAfter(Session oSession) { }

        public void AutoTamperResponseBefore(Session oSession)
        {
            if (_miEnableSpd.Checked || _miEnableRevertFromTemplate.Checked || _miEnableMasterPageEditing.Checked || _miEnableUrlStructure.Checked)
            {
                oSession.utilDecodeResponse();
            }

            if (_miEnableSpd.Checked)
            {
                if (oSession.uriContains(Constants.AuthorDll) && oSession.oResponse.headers.ExistsAndContains(Constants.ContentType, Constants.ApplicationRpc))
                {
                    oSession.utilReplaceInResponse(Constants.DisableWebdesignFeatures, string.Empty);
                    oSession.utilReplaceInResponse(string.Format(Constants.AllowDesigner, "0"), string.Format(Constants.AllowDesigner, "1"));
                }
            }

            if (_miEnableRevertFromTemplate.Checked)
            {
                if (oSession.uriContains(Constants.ProcessQuery) && oSession.oResponse.headers.ExistsAndContains(Constants.ContentType, Constants.ApplicationJson))
                {
                    oSession.utilReplaceInResponse(string.Format(Constants.AllowRevertFromTemplate, "false"), string.Format(Constants.AllowRevertFromTemplate, "true"));
                }
            }

            if (_miEnableMasterPageEditing.Checked)
            {
                if (oSession.uriContains(Constants.ProcessQuery) && oSession.oResponse.headers.ExistsAndContains(Constants.ContentType, Constants.ApplicationJson))
                {
                    oSession.utilReplaceInResponse(string.Format(Constants.AllowMasterPageEditing, "false"), string.Format(Constants.AllowMasterPageEditing, "true"));
                }
            }

            if (_miEnableUrlStructure.Checked)
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
            FiddlerApplication.UI.mnuMain.MenuItems.Add(_mnuSpd);
        }
    }
}
