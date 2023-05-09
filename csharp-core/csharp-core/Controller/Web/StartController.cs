using csharp_core.PageObjects.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_core.Controller.Web
{
    public class StartController : TestBase
    {
        public static bool CheckCodigoLogin()
        {
            return Driver.FindElements( StartPage.CodigoLogin()).Count>0;
        }
    }
}
