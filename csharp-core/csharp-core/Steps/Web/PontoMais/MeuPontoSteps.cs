using csharp_core.PageObjects.Web.PontoMais;
using OpenQA.Selenium.Interactions;

namespace csharp_core.Steps.Web.PontoMais
{
    public class MeuPontoSteps : TestBase
    {
        public static bool WaitLupa()
        {
            Logger = "Tela Meu Ponto";

            return MeuPontoPage.Lupa.Wait() != null;
        }

        public static void TypeComecandoEm(string text)
        {
            Logger = $"Começando em: {text}";
            new Actions(Driver).SendKeys(MeuPontoPage.ComecandoEm.Wait(), text).Build().Perform();
        }

        public static void TypeTerminandoEm(string text)
        {
            Logger = $"Terminando em: {text}";
            new Actions(Driver).SendKeys(MeuPontoPage.TerminandoEm.Wait(), text).Build().Perform();
        }

        public static void ClickLupa()
        {
            Logger = "Click Lupa";
            MeuPontoPage.Lupa.Click();
        }

        public static bool WaitResultado()
        {
            Logger = "Checkpoint Resultado";
            return MeuPontoPage.Resultado.Wait() != null;
        }
    }
}