using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_core.PageObjects.Web
{
    public class ClientIdentificationPage
    {
        public static By Usuario()
        {
           return By.Name("formLogin:j_id15");
        }
        public static By Senha()
        {
            return By.Name("formLogin:j_id17");
        }
        public static By Dominio()
        {
            return By.Name("formLogin:j_id19");
        }
        public static By Login()
        {
            return By.Name("formLogin:j_id30");
        }
    }
}
