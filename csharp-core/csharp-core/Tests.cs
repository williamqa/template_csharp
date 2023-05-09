using csharp_core.Controller.Mobile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using csharp_core.Controller.Web;
using csharp_core.Controller.Desktop;
using RestSharp;
using System.IO;
using System;
using csharp_core.Steps.Web.PontoMais;
using Newtonsoft.Json.Linq;

namespace csharp_core
{
    [TestClass]
    [Jira("ID", "URL", "USER", "TOKEN")]
    public class Tests : TestBase
    {
        [TestMethod]
        public void TestComFalha()
        {
            SetDriverType(DriverType.WEB, FrontEnd.CHROME);
            ChromeController.SetBarraDePesquisa("carros");
            ChromeController.ClickImagens();
            Checkpoint(false, "Test com falha");
        }

        [TestMethod]
        public void RestTestGet()
        {
            RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
            RestRequest request = new RestRequest("posts/42", Method.GET);
            IRestResponse response = client.Execute(request);
            Logger = "Response Body:";
            Checkpoint(response.Content.Contains("\"id\": 42,"), response.Content, false, true);
        }

        [TestMethod]
        public void RestTestPost()
        {
            RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
            RestRequest request = new RestRequest("/posts", Method.POST);
            request.AddHeader("Content-type", "application/json; charset=UTF-8");
            request.AddJsonBody(new { title = "foo", body = "bar", userId = 1 });
            IRestResponse response = client.Execute(request);
            Checkpoint(response.Content.Contains("\"userId\": 1,"), response.Content, false, true);
        }

        [TestMethod]
        public void AndroidTest()
        {
            SetDriverType(DriverType.MOBILE, FrontEnd.ANDROID);
            CalcController.ClickNumber1();
            CalcController.ClickAddition();
            CalcController.ClickNumber2();
            CalcController.ClickEquals();
            Checkpoint(CalcController.GetResult().Equals("RAD"), "Resultado da Pesquisa encontrado");
        }

        [TestMethod]
        public void DesktopTest()
        {
            SetDriverType(DriverType.DESKTOP, FrontEnd.WINDOWS);
            NotepadController.SetEditor("Ol�, eu sou um teste automatizado");
            Checkpoint(NotepadController.CheckResultado("automatizado"), "Resultado da Pesquisa encontrado");
        }

        [TestMethod]
        public void WebTest()
        {
            SetDriverType(DriverType.WEB, FrontEnd.CHROME);
            ChromeController.SetBarraDePesquisa("carros");
            ChromeController.ClickImagens();
            Checkpoint(ChromeController.CheckResultado(), "Resultado da Pesquisa encontrado");
        }

        //[TestMethod]
        public void DataBaseTest()
        {
            var sqlResult = ExecuteQuery("SqlConfig.json", "SqlQuery.sql", null, DataBaseType.SQL);
            var oracleResult = ExecuteQuery("OracleConfig.json", "OracleQuery.sql", null, DataBaseType.ORACLE);
        }

        [TestMethod]
        public void CrossTechnologyTest()
        {
            SetDriverType(DriverType.DESKTOP, FrontEnd.WINDOWS);
            NotepadController.SetEditor("Ol�, eu sou um teste automatizado");
            Checkpoint(NotepadController.CheckResultado("automatizado"), "Resultado da Pesquisa encontrado");

            SetDriverType(DriverType.MOBILE, FrontEnd.ANDROID);
            CalcController.ClickNumber1();
            CalcController.ClickAddition();
            CalcController.ClickNumber2();
            CalcController.ClickEquals();
            Checkpoint(CalcController.GetResult().Equals("RAD"), "Resultado");

            SetDriverType(DriverType.WEB, FrontEnd.CHROME);
            ChromeController.SetBarraDePesquisa("carros");
            ChromeController.ClickImagens();
            Checkpoint(ChromeController.CheckResultado(), "Resultado da Pesquisa encontrado");

            RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
            RestRequest request = new RestRequest("posts/42", Method.GET);
            IRestResponse response = client.Execute(request);
            Checkpoint(response.Content.Contains("\"id\": 42,"), response.Content, false, true);
        }

        [TestMethod]
        public void PontoMaisTest()
        {
            var data = Utils.GetData("PontoMaisData.json");
            var emailOuCpf = data.GetToken("EmailOuCpf");
            var senha = data.GetToken("Senha");
            DateTime now = DateTime.Now;
            string today = now.ToString("dd/MM/yyyy");
            string firstDay = now.AddDays(1).AddDays(-now.Day).ToString("dd/MM/yyyy");

            SetDriverType(DriverType.WEB, FrontEnd.CHROME, "WebConfigPontoMais.json");

            LoginSteps.TypeEmailOuCpf(emailOuCpf);
            LoginSteps.TypeSenha(senha);
            LoginSteps.ClickEntrar();
            RegistroDePontoSteps.ClickMeuPonto();
            MeuPontoSteps.TypeComecandoEm(firstDay);
            MeuPontoSteps.TypeTerminandoEm(today);
            MeuPontoSteps.ClickLupa();

            Checkpoint(MeuPontoSteps.WaitResultado(), "Checkpoint Resultado");
        }
    }
}