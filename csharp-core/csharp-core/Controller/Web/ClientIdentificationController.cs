using csharp_core.PageObjects.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_core.Controller.Web
{
    public class ClientIdentificationController : TestBase
    {
        public static void SetUsuario(string text)
        {
            Logger = $"Usuario:{text}";
            ClientIdentificationPage.Usuario().SendKeys(text);
        }
        public static void SetSenha(string text)
        {
            Logger = $"Senha:{text}";
            ClientIdentificationPage.Senha().SendKeys(text);
        }
        public static void SetDominio(string text)
        {
            Logger = $"Dominio:{text}";
            ClientIdentificationPage.Dominio().SendKeys(text);
        }
        public static void ClickLogin()
        {
            Logger = $"Click Login ";
            ClientIdentificationPage.Login().Click();
        }
    }
}
