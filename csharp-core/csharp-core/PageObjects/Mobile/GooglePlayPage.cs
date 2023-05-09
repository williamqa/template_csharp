using OpenQA.Selenium;

namespace fapc_core.PageObjects.Mobile
{
    public class GooglePlayPage
    {
        public static By Pesquisar()
        {
            return By.XPath("//android.widget.ImageView[@resource-id='com.android.vending:id/search_box_idle_text']");
        }

        public static By PesquisarNoGoogle()
        {
            return By.XPath("//android.widget.EditText[@resource-id='com.android.vending:id/search_box_text_input']");
        }

        public static By Resultado()
        {
            return By.XPath("//android.widget.TextView[@resource-id='com.android.vending:id/suggest_text']");
        }

        public static By Aplicativo()
        {
            return By.XPath("//android.widget.TextView[@resource-id='com.android.vending:id/title_title']");
        }
    }
}