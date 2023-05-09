using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_core.PageObjects.Web
{
    public class StartPage
    {
        public static By CodigoLogin()
        {
            return By.Id("usuario");
        }
    }
}
