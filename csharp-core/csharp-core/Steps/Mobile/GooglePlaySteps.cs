using fapc_core.Controller.Mobile;
using TechTalk.SpecFlow;

namespace fapc_core.Steps.Mobile
{
    [Binding]
    public class GooglePlaySteps : TestBase
    {
        [Given(@"Eu estou na Tela Principal")]
        public void GivenEuEstouNaTelaPrincipal()
        {
            SetDriverType(DriverType.MOBILE, FrontEnd.ANDROID);
        }
        
        [Given(@"Eu clico em pesquisar")]
        public void GivenEuClicoEmPesquisar()
        {
            GooglePlayController.ClickPesquisar();
        }

        [Given(@"Eu informo um termo de busca (.*)")]
        public void GivenEuInformoUmTermoDeBusca(string termo)
        {
            GooglePlayController.SetPesquisar(termo);
        }

        [When(@"Eu clico no resultado")]
        public void WhenEuClicoNoResultado()
        {
            GooglePlayController.ClickResultado();
        }
        
        [Then(@"Sou direcionado para a tela do Aplicativo")]
        public void ThenSouDirecionadoParaATelaDoAplicativo()
        {
            GooglePlayController.Resultado();
        }
    }
}
