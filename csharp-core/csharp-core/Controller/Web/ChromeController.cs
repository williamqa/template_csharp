using fapc_core.PageObjects.Web;
using OpenQA.Selenium;

namespace fapc_core.Controller.Web
{
    public class ChromeController : TestBase
    {
        public static void SetBarraDePesquisa(string text)
        {
            Logger = $"Pesquisar: {text}";
            ChromePage.BarraDePesquisa().SendKeys(text + Keys.Enter);
        }

        public static void ClickImagens()
        {
            Logger = "Click Imagens";
            ChromePage.Imagens().Click(30);
        }

        public static bool CheckResultado()
        {
            return Driver.FindElements(ChromePage.Resultado()).Count > 0;
        }
    }
}