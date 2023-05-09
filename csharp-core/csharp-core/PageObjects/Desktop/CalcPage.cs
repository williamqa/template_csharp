using OpenQA.Selenium;

namespace csharp_core.PageObjects.Desktop
{
    public class CalcPage
    {
        public static By Equal()
        {
            return By.XPath($"//Button[@AutomationId='equalButton']");
        }
        public static By Plus()
        {
            return By.XPath($"//Button[@AutomationId='plusButton']");
        }
        public static By ButtonX(int num)
        {
            return By.XPath($"//Button[@AutomationId='num{num}Button']");
        }
        public static By Button0()
        {
            return By.XPath("//Button[@AutomationId='num0Button']");
        }
        public static By Button1()
        {
            return By.XPath("//Button[@AutomationId='num1Button']");
        }
        public static By Button2()
        {
            return By.XPath("//Button[@AutomationId='num2Button']");
        }
        public static By Button3()
        {
            return By.XPath("//Button[@AutomationId='num3Button']");
        }
        public static By Button4()
        {
            return By.XPath("//Button[@AutomationId='num4Button']");
        }
        public static By Button5()
        {
            return By.XPath("//Button[@AutomationId='num5Button']");
        }
        public static By Button6()
        {
            return By.XPath("//Button[@AutomationId='num6Button']");
        }
        public static By Button7()
        {
            return By.XPath("//Button[@AutomationId='num7Button']");
        }
        public static By Button8()
        {
            return By.XPath("//Button[@AutomationId='num8Button']");
        }
        public static By Button9()
        {
            return By.XPath("//Button[@AutomationId='num9Button']");
        }
    }
}