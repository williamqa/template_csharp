using OpenQA.Selenium;

namespace fapc_core.PageObjects.Web.PontoMais
{
    public class MeuPontoPage
    {
        public static By Lupa = By.XPath("//i[@material-icon='search']");
        public static By ComecandoEm = By.Name("start_date");
        public static By TerminandoEm = By.Name("end_date");
        public static By Resultado = By.XPath("//*[@id='content-wrapper']/div[2]/div/ng-view/div[2]/div[2]/table/tbody/tr[2]/td[3]/i");
    }
}