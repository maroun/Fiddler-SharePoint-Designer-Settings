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
            throw new NotImplementedException();
        }
    }
}
