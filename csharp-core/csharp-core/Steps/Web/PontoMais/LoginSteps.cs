using csharp_core.PageObjects.Web.PontoMais;

namespace csharp_core.Steps.Web.PontoMais
{
    public class LoginSteps : TestBase
    {
        public static void TypeEmailOuCpf(string text)
        {
            Logger = "Seu e-mail ou CPF";
            LoginPage.EmailOuCpf.SendKeys(text);
        }

        public static void TypeSenha(string text)
        {
            Logger = "Sua Senha";
            LoginPage.Senha.SendKeys(text);
        }

        public static void ClickEntrar()
        {
            Logger = "Click Entrar";
            LoginPage.Entrar.Click();
        }
    }
}