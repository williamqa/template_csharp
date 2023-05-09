using fapc_core.Controller.Web;
using TechTalk.SpecFlow;

namespace fapc_core.Steps.Web
{
    [Binding]
    public class GoogleSteps : TestBase
    {
        [Given(@"Acessar Google")]
        public void GivenAcessarGoogle()
        {
            SetDriverType(DriverType.WEB, FrontEnd.CHROME);
        }
        
        [Given(@"Pesquisar (.*)")]
        public void GivenPesquisarCarro(string termo)
        {
            ChromeController.SetBarraDePesquisa(termo);
        }
        
        [When(@"Clicar em Imagens")]
        public void WhenClicarEmImagens()
        {
            ChromeController.ClickImagens();
        }
        
        [Then(@"Apresentar Resultado")]
        public void ThenApresentarResultado()
        {
            Checkpoint(ChromeController.CheckResultado(), "Resultado da Pesquisa encontrado");
        }
    }
}