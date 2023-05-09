using csharp_core.PageObjects.Mobile;

namespace csharp_core.Controller.Mobile
{
    public class CalcController : TestBase
    {
        public static void ClickNumber0()
        {
            Logger = "Click on number 0";
            CalcPage.Number0().Click();
        }

        public static void ClickNumber1()
        {
            Logger = "Click on number 1";
            CalcPage.Number1().Click();
        }

        public static void ClickNumber2()
        {
            Logger = "Click on number 2";
            CalcPage.Number2().Click();
        }

        public static void ClickNumber3()
        {
            Logger = "Click on number 3";
            CalcPage.Number3().Click();
        }

        public static void ClickNumber4()
        {
            Logger = "Click on number 4";
            CalcPage.Number4().Click();
        }

        public static void ClickNumber5()
        {
            Logger = "Click on number 5";
            CalcPage.Number5().Click();
        }

        public static void ClickNumber6()
        {
            Logger = "Click on number 6";
            CalcPage.Number6().Click();
        }

        public static void ClickNumber7()
        {
            Logger = "Click on number 7";
            CalcPage.Number7().Click();
        }

        public static void ClickNumber8()
        {
            Logger = "Click on number 8";
            CalcPage.Number8().Click();
        }

        public static void ClickNumber9()
        {
            Logger = "Click on number 9";
            CalcPage.Number9().Click();
        }

        public static void ClickAddition()
        {
            Logger = "Click Addition";
            CalcPage.Addition().Click();
        }

        public static void ClickSubtraction()
        {
            Logger = "Click Subtraction";
            CalcPage.Subtraction().Click();
        }

        public static void ClickMultiplication()
        {
            Logger = "Click Multiplication";
            CalcPage.Multiplication().Click();
        }

        public static void ClickDivision()
        {
            Logger = "Click Division";
            CalcPage.Division().Click();
        }

        public static void ClickDotOrComma()
        {
            Logger = "Click Dot Or Comma";
            CalcPage.DotOrComma().Click();
        }

        public static void ClickEquals()
        {
            Logger = "Click Equals";
            CalcPage.Equals().Click();
        }

        public static string GetResult()
        {
            return CalcPage.Result().Text();
        }
    }
}