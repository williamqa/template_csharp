using OpenQA.Selenium;

namespace csharp_core.PageObjects.Web
{
    public class ChromePage
    {
        public static By BarraDePesquisa()
        {
            return By.Name("q");
        }

        public static By Imagens()
        {
            return By.LinkText("Imagens");
        }

        public static By Resultado()
        {
            return By.TagName("img");
        }
    }
}