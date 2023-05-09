using OpenQA.Selenium;

namespace csharp_core.PageObjects.Web.PontoMais
{
    public class LoginPage
    {
        public static By EmailOuCpf = By.Name("login");
        public static By Senha = By.Name("password");
        public static By Entrar = By.XPath("//button[@type='submit']");
    }
}