using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;

namespace csharp_core.Examples.Features.Service
{
    [TestClass]
    public class RestTestFeature : Hooks
    {
        [TestMethod]
        public void RestTestGet()
        {
            SetDriverType(DriverType.SERVICE, FrontEnd.REST, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Examples", "Deploy", "RestConfig.json"));

            Request = new RestRequest("posts/42", Method.GET);

            Response = Client.Execute(Request);

            Checkpoint(Response.Content.Contains("\"id\": 42,"), Response.Content, false, true);

        }
        [TestMethod]
        public void RestTestPost()
        {
            SetDriverType(DriverType.SERVICE, FrontEnd.REST, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Examples", "Deploy", "RestConfig.json"));

            Request = new RestRequest("/posts", Method.POST);
            Request.AddHeader("Content-type", "application/json; charset=UTF-8");
            Request.AddJsonBody(new { title = "foo", body = "bar", userId = 1 });

            Response = Client.Execute(Request);

            Checkpoint(Response.Content.Contains("\"userId\": 1,"), Response.Content, false, true);

        }

        public string GetToken(string login, string senha)
        {
            SetDriverType(DriverType.SERVICE, FrontEnd.REST, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Examples", "Deploy", "RestConfig.json"));

            Request = new RestRequest("/grb/sb/credencial/autenticacao?client_id=guid_number", Method.POST);

            Request.AddHeader("unidadeNegocio", "EUD");
            Request.AddQueryParameter("client_id", "guid_number");
            Request.AddJsonBody(new { login = login, senha = senha, origem = "GERASGI_EUD" });

            Response = Client.Execute(Request);

            var tokenAutorizacao = Response.Content.GetToken("tokenAutorizacao");

            tokenAutorizacao = tokenAutorizacao.Substring(0, tokenAutorizacao.Length - 15);

            Console.WriteLine($"tokenAutorizacao: {tokenAutorizacao}");

            Checkpoint(Response.Content.Contains("tokenAutorizacao"), Response.Content, false, true);

            return tokenAutorizacao;
        }

        [TestMethod]
        public void MyTest()
        {
            var token = GetToken("USR", "PSW");
        }
    }
}