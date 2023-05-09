using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace csharp_core
{
    public static class Utils
    {
        public static void JsScrollIntoView(this By by, int timeout = TestBase.DefaultTimeout, bool failIfNotExists = false)
        {
            var element = by.Wait(timeout, failIfNotExists);

            if (element != null)
            {
                TestBase.Checkpoint(true, $"Scroll Into View", false, false);

                IJavaScriptExecutor js = TestBase.Driver;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
        }

        public static void JsHidden(this By by, int timeout = TestBase.DefaultTimeout, bool failIfNotExists = false)
        {
            var element = by.Wait(timeout, failIfNotExists);

            if (element != null)
            {
                TestBase.Checkpoint(true, $"Hidden", false, false);

                IJavaScriptExecutor js = TestBase.Driver;
                js.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", element, "style", "visibility: hidden;");
            }
        }

        public static void JsSetAttribute(this By by, string attributeName, string attributeValue, int timeout = TestBase.DefaultTimeout, bool failIfNotExists = false)
        {
            var element = by.Wait(timeout, failIfNotExists);

            if (element != null)
            {
                TestBase.Checkpoint(true, $"Set Attributte: {attributeName} {attributeValue}", false, false);

                IJavaScriptExecutor js = TestBase.Driver;
                js.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", element, attributeName, attributeValue);
            }
        }

        public static string GetData(string fileName)
        {
            return File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName));
        }

        public static string GetToken(this string jsonBody, string token)
        {
            if (string.IsNullOrEmpty(jsonBody) || string.IsNullOrEmpty(token))
            {
                return "";
            }

            try
            {
                return JObject.Parse(jsonBody).SelectToken(token).Value<string>();
            }
            catch
            {
                return "";
            }
        }

        public static void LongPress(this By by, int timeout = TestBase.DefaultTimeout, bool failIfNotExists = true)
        {
            var element = by.Wait(timeout, failIfNotExists);

            if (element != null)
            {
                new TouchAction((IPerformsTouchActions)TestBase.Driver).LongPress(element).Perform();
            }
        }

        public static void SenKeysNext(this AndroidDriver<AndroidElement> androidDriver)
        {
            androidDriver.ExecuteScript("mobile: performEditorAction", new Dictionary<string, string> { { "action", "next" } });
        }

        public static IWebElement Wait(this By by, int timeout = TestBase.DefaultTimeout, bool failIfNotExists = true)
        {
            for (int i = 0; i < timeout; i++)
            {
                try
                { 
                    if (TestBase.Driver.FindElement(by).Displayed)
                    {
                        TestBase.Checkpoint(true, $"Elemento encontrado pelo localizador {by}");

                        return TestBase.Driver.FindElement(by);
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }

            if (failIfNotExists)
            {
                TestBase.Checkpoint(false, $"Elemento não encontrado pelo localizador {by}");
            }

            return null;
        }

        public static void Click(this By by, int timeout = TestBase.DefaultTimeout, bool failIfNotExists = true)
        {
            try
            {
                TestBase.AndroidDriver.HideKeyboard();
            }
            catch { }

            var element = by.Wait(timeout, failIfNotExists);

            if (element != null)
            {
                element.Click();
            }
        }

        public static void SendKeys(this By by, string text, int timeout = TestBase.DefaultTimeout, bool failIfNotExists = true)
        {
            var element = by.Wait(timeout, failIfNotExists);

            if (element != null)
            {
                try
                {
                    element.Clear();
                }
                catch
                {

                }
                element.SendKeys(text);
            }
        }

        public static string Text(this By by, int timeout = TestBase.DefaultTimeout, bool failIfNotExists = true)
        {
            var element = by.Wait(timeout, failIfNotExists);

            if (element != null)
            {
                return element.Text;
            }

            return "";
        }
    }
}