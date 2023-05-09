using fapc_core.Controller.Web;
using System;
using TechTalk.SpecFlow;

namespace fapc_core.Steps.Web
{
    [Binding]
    public class LoginSiagSteps : TestBase
    {
        [Given(@"Estar na tela de login")]
        public void GivenEstarNaTelaDeLogin()
        {
            SetDriverType(DriverType.WEB, FrontEnd.CHROME);
        }
        
        [Given(@"Informar o usuario (.*)")]
        public void GivenInformarOUsuario(int p0)
        {
            ClientIdentificationController.SetUsuario(Ambiente.User);
        }
        
        [Given(@"Informar a senha (.*)")]
        public void GivenInformarASenha(int p0)
        {
            ClientIdentificationController.SetSenha(Ambiente.Password);

        }
        
        [Given(@"Informar o dominio Clientes")]
        public void GivenInformarODominioClientes()
        {
            ClientIdentificationController.SetDominio(Ambiente.Dominio);
        }
        
        [When(@"Pressionar login")]
        public void WhenPressionarLogin()
        {
            ClientIdentificationController.ClickLogin();
        }
        
        [Then(@"O campo codigo login e apresentado")]
        public void ThenOCampoCodigoLoginEApresentado()
        {
            Checkpoint(StartController.CheckCodigoLogin(), "Login realizado com sucesso. ");
        }
    }
}
