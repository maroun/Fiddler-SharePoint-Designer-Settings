using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FiddlerSPDSettings
{
    public class SPDSettings : IAutoTamper2
    {
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
