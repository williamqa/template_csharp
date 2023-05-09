using OpenQA.Selenium;

namespace fapc_core.PageObjects.Desktop
{
    public class NotepadPage
    {
        public static By Arquivo = By.XPath("/Window/MenuBar/MenuItem[1]");
        public static By Editar = By.XPath("/Window/MenuBar/MenuItem[2]");
        public static By Formatar = By.XPath("/Window/MenuBar/MenuItem[3]");
        public static By Exibir = By.XPath("/Window/MenuBar/MenuItem[4]");
        public static By Ajuda = By.XPath("/Window/MenuBar/MenuItem[5]");
        public static By Editor = By.XPath("/Window/Edit");
    }
}