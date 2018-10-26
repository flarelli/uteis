using AutomacaoBDD.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Threading;

namespace AutomacaoBDD.Helpers
{
    public class WaitUntil
    {
        static double TIMEOUT = Convert.ToDouble(ConfigurationManager.AppSettings["DEFAULT_TIMEOUT"]);
        static int TIMEOUTBETWEENEVENTS = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultTimeoutBetweenEvents"]);

        public static IWebElement ElementToBeClickable(IWebElement element)
        {
            try
            {
                Thread.Sleep(TIMEOUTBETWEENEVENTS);
                return new WebDriverWait(DriverFactory.INSTANCE, TimeSpan.FromSeconds(TIMEOUT)).Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (Exception e)
            {
                var path = GeneralHelpers.TakeScreenshot("Erro");
                //Gravar log de Falha do Assert
                TestBase.FailTest(Utilitarios.GetCurrentMethod() + " => " + "ERRO! Elemento esperado não apareceu." + "<pre>" + e.Message + "</pre>");
                
                DriverFactory.INSTANCE.Quit();
                throw new Exception("SCREENSHOT GENERATED => " + "url(" + path + ")", e.InnerException);
            }
        }

        public static IWebElement ElementToBeClickable(By element)
        {
            try
            {
                Thread.Sleep(TIMEOUTBETWEENEVENTS);
                return new WebDriverWait(DriverFactory.INSTANCE, TimeSpan.FromSeconds(TIMEOUT)).Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (Exception e)
            {
                var path = GeneralHelpers.TakeScreenshot("Erro");
                TestBase.FailTest(Utilitarios.GetCurrentMethod() + " => " + "ERRO! Elemento esperado não apareceu." + "<pre>" + e.Message + "</pre>");
                DriverFactory.INSTANCE.Quit();
                throw new Exception("SCREENSHOT GENERATED => " + "url(" + path + ")", e.InnerException);
            }
        }

        public static IWebElement ElementToBeVisible(By element)
        {
            try
            {
                Thread.Sleep(TIMEOUTBETWEENEVENTS);
                return new WebDriverWait(DriverFactory.INSTANCE, TimeSpan.FromSeconds(TIMEOUT)).Until(ExpectedConditions.ElementIsVisible(element));
            }
            catch (Exception e)
            {
                var path = GeneralHelpers.TakeScreenshot("Erro");
                TestBase.FailTest(Utilitarios.GetCurrentMethod() + " => " + "ERRO! Elemento esperado não apareceu." + "<pre>" + e.Message + "</pre>");
                DriverFactory.INSTANCE.Quit();
                throw new Exception("SCREENSHOT GENERATED => " + "url(" + path + ")", e.InnerException);
            }
        }

        public static bool ElementExists(By element)
        {
            try
            {
                Thread.Sleep(TIMEOUTBETWEENEVENTS);
                DriverFactory.INSTANCE.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

                if (new WebDriverWait(DriverFactory.INSTANCE, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.ElementExists(element)) != null &&
                    new WebDriverWait(DriverFactory.INSTANCE, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.ElementExists(element)).Size.ToString() != "0")
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                DriverFactory.INSTANCE.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIMEOUT);
            }
        }
        public static bool ElementisInvisibility(By element)
        {
            try
            {
                Thread.Sleep(TIMEOUTBETWEENEVENTS);
                DriverFactory.INSTANCE.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

                if (new WebDriverWait(DriverFactory.INSTANCE, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.InvisibilityOfElementLocated(element)))
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                DriverFactory.INSTANCE.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIMEOUT);
            }
        }
    }
}
