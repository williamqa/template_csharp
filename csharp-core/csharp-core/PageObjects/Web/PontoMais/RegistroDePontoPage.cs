using OpenQA.Selenium;

namespace csharp_core.PageObjects.Web.PontoMais
{
    public class RegistroDePontoPage
    {
        public static By RegistrarPonto = By.XPath("//button[@ng-click='save()']");
        public static By MeuPonto = By.XPath("//a[@href='#/meu_ponto'][2]");
    }
}