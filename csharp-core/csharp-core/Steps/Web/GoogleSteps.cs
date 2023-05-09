using csharp_core.Controller.Web;
using TechTalk.SpecFlow;

namespace csharp_core.Steps.Web
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