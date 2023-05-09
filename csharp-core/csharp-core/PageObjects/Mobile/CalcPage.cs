using OpenQA.Selenium;

namespace fapc_core.PageObjects.Mobile
{
    public class CalcPage
    {
        public static By Number0()
        {
            return By.XPath("//android.widget.Button[@text='0']");
        }

        public static By Number1()
        {
            return By.XPath("//android.widget.Button[@text='1']");
        }

        public static By Number2()
        {
            return By.XPath("//android.widget.Button[@text='2']");
        }

        public static By Number3()
        {
            return By.XPath("//android.widget.Button[@text='3']");
        }

        public static By Number4()
        {
            return By.XPath("//android.widget.Button[@text='4']");
        }

        public static By Number5()
        {
            return By.XPath("//android.widget.Button[@text='5']");
        }

        public static By Number6()
        {
            return By.XPath("//android.widget.Button[@text='6']");
        }

        public static By Number7()
        {
            return By.XPath("//android.widget.Button[@text='7']");
        }

        public static By Number8()
        {
            return By.XPath("//android.widget.Button[@text='8']");
        }

        public static By Number9()
        {
            return By.XPath("//android.widget.Button[@text='9']");
        }

        public static By Addition()
        {
            return By.XPath("//android.widget.Button[@text='+']");
        }

        public static By Subtraction()
        {
            return By.XPath("//android.widget.Button[@text='−']");
        }

        public static By Multiplication()
        {
            return By.XPath("//android.widget.Button[@text='×']");
        }

        public static By Division()
        {
            return By.XPath("//android.widget.Button[@text='÷']");
        }

        public static By Equals()
        {
            return By.XPath("//android.widget.Button[@text='=']");
        }

        public static By DotOrComma()
        {
            return By.XPath("//android.widget.Button[@text=',' or @text='.']");
        }

        public static By Result()
        {
            return By.XPath("//android.widget.TextView");
        }
    }
}