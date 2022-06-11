using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using TrainTickets;
using TrainTickets.View.TrainRoutes;

namespace HelpSystem
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlHelper
    {
        TrainRoutesPage prozor;
        public JavaScriptControlHelper(TrainRoutesPage w)
        {
            prozor = w;
        }

    }
}
