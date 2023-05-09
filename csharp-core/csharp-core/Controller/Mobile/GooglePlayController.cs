using fapc_core.PageObjects.Mobile;

namespace fapc_core.Controller.Mobile
{
    public class GooglePlayController : TestBase
    {
        public static void Resultado()
        {
            Logger = "Aplicativo";
            GooglePlayPage.Aplicativo().Wait();
        }

        public static void ClickResultado()
        {
            Logger = "Click Resultado";
            GooglePlayPage.Resultado().Click();
        }

        public static void ClickPesquisar()
        {
            Logger = "Click Pesquisar";
            GooglePlayPage.Pesquisar().Click();
        }

        public static void SetPesquisar(string text)
        {
            Logger = $"Pesquisar: {text}";
            GooglePlayPage.PesquisarNoGoogle().SendKeys(text);
        }
    }
}