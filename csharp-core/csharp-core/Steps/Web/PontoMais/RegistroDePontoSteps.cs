using csharp_core.PageObjects.Web.PontoMais;

namespace csharp_core.Steps.Web.PontoMais
{
    public class RegistroDePontoSteps : TestBase
    {
        public static bool WaitRegistrarPonto()
        {
            Logger = "Registrar Ponto";

            return RegistroDePontoPage.RegistrarPonto.Wait(DefaultTimeout, false) != null;
        }

        public static void ClickMeuPonto()
        {
            Logger = "Click Meu Ponto";
            RegistroDePontoPage.MeuPonto.Click();
        }
    }
}