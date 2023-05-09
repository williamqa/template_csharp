using fapc_core.PageObjects.Desktop;
using System.Diagnostics;
using System.Threading;

namespace fapc_core.Controller.Desktop
{
    public class NotepadController : TestBase
    {
        public static void ClickArquivo()
        {
            Logger = "Click Arquivo";
            NotepadPage.Arquivo.Click();
            NotepadPage.Arquivo.Click();
        }
        public static void ClickEditar()
        {
            Logger = "Click Editar";
            NotepadPage.Editar.Click();
            NotepadPage.Editar.Click();
        }
        public static void ClickFormatar()
        {
            Logger = "Click Formatar";
            NotepadPage.Formatar.Click();
            NotepadPage.Formatar.Click();
        }
        public static void ClickExibir()
        {
            Logger = "Click Exibir";
            NotepadPage.Exibir.Click();
            NotepadPage.Exibir.Click();
        }
        public static void ClickAjuda()
        {
            Logger = "Click Ajuda";
            NotepadPage.Ajuda.Click();
            NotepadPage.Ajuda.Click();
        }
        public static void SetEditor(string text)
        {
            Logger = $"Set Editor: {text}";
            NotepadPage.Editor.Click();
            Thread.Sleep(1000);
            NotepadPage.Editor.SendKeys(text);
        }
        public static bool CheckResultado(string text)
        {
            var result = NotepadPage.Editor.Text().Contains(text);

            Process.Start("cmd", "/C taskkill /im notepad.exe /f /t");

            return result;
        }
    }
}