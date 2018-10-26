using AutomacaoBDD.Functions;
using AutomacaoBDD.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomacaoBDD.Helpers
{
    public class PageBase
    {
        protected WebDriverWait wait { get; private set; }
        protected IWebDriver driver { get; private set; }
        protected IJavaScriptExecutor javaScript { get; private set; }

        public PageBase()
        {
            PageFactory.InitElements(DriverFactory.INSTANCE, this);
            wait = new WebDriverWait(DriverFactory.INSTANCE, TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["DEFAULT_TIMEOUT"])));
            driver = DriverFactory.INSTANCE;
            javaScript = (IJavaScriptExecutor)driver;
        }

        public void Refresh()
        {
            DriverFactory.INSTANCE.Navigate().Refresh();
        }

        public void NavigateTo(string url)
        {
            DriverFactory.INSTANCE.Navigate().GoToUrl(url);
        }

        public void SendKeysJavaScript(IWebElement element, string value)
        {
            javaScript.ExecuteScript("arguments[0].setAttribute=('value', arguments[1])", element, value);
        }

        public void SelectByTextJavaScript(IWebElement element, string value)
        {
            javaScript.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", element, value);
        }

        public static bool Exists(By locator)
        {
            return WaitUntil.ElementExists(locator);
        }

        public static void Click(IWebElement element)
        {
            //IWebElement e = WaitUntil.ElementToBeClickable(element);
            //GeneralHelpers.ScrollTo(GetElement(element));
            //new Actions(DriverFactory.INSTANCE).MoveToElement(e).Click().Perform();
            GetElement(element).Click();
        }

        public static void Click(By locator)
        {
            //IWebElement e = WaitUntil.ElementToBeClickable(locator);
            //GeneralHelpers.ScrollTo(GetElement(locator));
            //new Actions(DriverFactory.INSTANCE).MoveToElement(e).Click().Perform();
            
            GetElement(locator).Click();
        }

        public static void SendKey(IWebElement element, string value)
        {
            //WaitUntil.ElementToBeClickable(element).SendKeys(value + OpenQA.Selenium.Keys.Tab);
            element.SendKeys(value + OpenQA.Selenium.Keys.Tab);
        }

        public static void SendKeyInt(IWebElement element, int value)
        {
            //WaitUntil.ElementToBeClickable(element).SendKeys(value + OpenQA.Selenium.Keys.Tab);
            element.SendKeys(value + OpenQA.Selenium.Keys.Tab);
        }

        public static void SendKeyDouble(IWebElement element, double value)
        {
            //WaitUntil.ElementToBeClickable(element).SendKeys(value + OpenQA.Selenium.Keys.Tab);
            element.SendKeys(value + OpenQA.Selenium.Keys.Tab);
        }

        public static void Type(By locator, string value)
        {
            //WaitUntil.ElementToBeClickable(locator).SendKeys(value + OpenQA.Selenium.Keys.Tab);
            GetElement(locator).SendKeys(value + OpenQA.Selenium.Keys.Tab);
        }

        public static void Switch()
        {
            DriverFactory.INSTANCE.SwitchTo().Window(DriverFactory.INSTANCE.WindowHandles.Last());            
        }

        public static void RetornaPrimeiraPagina()
        {
            DriverFactory.INSTANCE.SwitchTo().Window(DriverFactory.INSTANCE.WindowHandles.First());
        }

        public static IWebElement GetElement(IWebElement element)
        {
            //return WaitUntil.ElementToBeClickable(element);
            return element;
        }

        public static IWebElement GetElement(By locator)
        {
            return WaitUntil.ElementToBeClickable(locator);            
        }
        public static IWebElement GetElementOculto(By locator)
        {
            return WaitUntil.ElementToBeClickable(locator);
        }

        public static string GetElementText(IWebElement element)
        {
            //return WaitUntil.ElementToBeClickable(element).GetAttribute("Text");
            return element.GetAttribute("Text");
        }

        public static string GetElementTextValue(IWebElement element)
        {
            //return WaitUntil.ElementToBeClickable(element).GetAttribute("Value");
            return element.GetAttribute("Value");
        }

        public static string RetornaTextLabel(By element)
        {
            return DriverFactory.INSTANCE.FindElement(element).Text;
        }
        public static bool ValidaElementTextLabel(By element, string value)
        {
            //return WaitUntil.ElementToBeVisible(element).Text.Contains(value);
            return GetElement(element).Text.Contains(value);
        }        

        public static void Wait(IWebElement element)
        {
            WaitUntil.ElementToBeClickable(element);
        }

        public static void Wait(By locator)
        {
            WaitUntil.ElementToBeClickable(locator);
        }

        public static void Clear(IWebElement element)
        {
            //WaitUntil.ElementToBeClickable(element).Clear();
            element.Clear();
        }

        public static void Clear(By locator)
        {
            //WaitUntil.ElementToBeClickable(locator).Clear();
            GetElement(locator).Clear();
        }

        public static void Select(IWebElement element, string value)
        {
            new SelectElement(GetElement(element)).SelectByText(value);
        }

        public static void SelectValue(IWebElement element, string value)
        {
            new SelectElement(GetElement(element)).SelectByValue(value);
        }

        public static void SelectIndex(IWebElement element, int value)
        {
            new SelectElement(GetElement(element)).SelectByIndex(value);
        }

        public static void Select(By locator, string value)
        {
            new SelectElement(GetElement(locator)).SelectByText(value);
        }

        public static string GetSelectedText(IWebElement element)
        {
            return new SelectElement(GetElement(element)).SelectedOption.Text;
        }

        public static string GetSelectedText(By locator)
        {
            return new SelectElement(GetElement(locator)).SelectedOption.Text;
        }

        public static List<string> GetAllOptionsCombo(By locator)
        {
            SelectElement oSelect = new SelectElement(GetElement(locator));
            IList<IWebElement> elementCount = oSelect.Options;
            List<string> allOptions = new List<string>();

            int iSize = elementCount.Count;

            for (int i = 0; i < iSize; i++)
            {
                allOptions.Add(elementCount.ElementAt(i).Text);
            }
            return allOptions;
        }
        public static List<string> GetAllOptionsComboValue(By locator)
        {
            IWebElement drop_down = DriverFactory.INSTANCE.FindElement(locator);
            SelectElement se = new SelectElement(drop_down);
            List<string> allOptions = new List<string>();

            for (int i = 0; i < se.Options.Count; i++)
                allOptions.Add(se.Options.ElementAt(i).GetAttribute("value"));
            return allOptions;
        }
        public static bool IsEnabled(By element)
        {
            //return WaitUntil.ElementToBeVisible(element).Enabled;
            return GetElement(element).Enabled;
        }

        public static bool IsVisible(By element)
        {
            return WaitUntil.ElementExists(element);
        }

        public static bool IsInvisibilit(By element)
        {
            return WaitUntil.ElementisInvisibility(element);
        }

        public static bool CampoOculto(By element)
        {
            var style = DriverFactory.INSTANCE.FindElement(element).GetAttribute("style");
            if (style.Contains("display: none;"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int Random(int de, int ate)
        {
            return new Random().Next(de, ate);
        }   
    }
}
