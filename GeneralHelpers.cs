using AutomacaoBDD.Pages.SimuladorPortabilidade;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomacaoBDD.Helpers
{
    
    public class GeneralHelpers
    {
        public static Dictionary<string, string> ToDictionary(Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            return dictionary;
        }
        public static int Random(int from, int to)
        {
            return new Random().Next(from, to);
        }

        public static string GetCurrentDate(string format, string addDays = "0")
        {
            return DateTime.Today.AddDays(Convert.ToDouble(addDays)).ToString(format);
        }

        public static void ScrollTo(IWebElement element)
        {
            ((IJavaScriptExecutor)DriverFactory.INSTANCE).ExecuteScript("arguments[0].scrollIntoView(true); ", element);
            Thread.Sleep(500);
        }

        
            
        public static string fileName { set; get; }
        public static string localArquivoCompleto;
        public static string nomeArquivoCompleto;
        public static string TakeScreenshot(string pasta)
        {
            var feature = FeatureContext.Current.FeatureInfo.Title.ToString().Trim().Replace(" ", "");
            var scenario = ScenarioContext.Current.ScenarioInfo.Title.ToString().Trim().Replace(" ", "");

            var tempo = string.Format("{0:dd-MM-yyyy-hh-mm-ss}", DateTime.Now);
            var dateTime = GetCurrentDate("dd-MM-yyyy");
            var localArquivoBase = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", pasta);

            localArquivoCompleto = string.Format("{0}\\{1}\\{2}\\{3}", localArquivoBase, feature, scenario, dateTime);

            if (!Directory.Exists(localArquivoCompleto))
            {
                Directory.CreateDirectory(localArquivoCompleto);
            }

            ScreenshotImageFormat extensaoArquivo = ScreenshotImageFormat.Jpeg;

            List<string> arquivos = new List<string>(Directory.GetFiles(localArquivoCompleto));
            var nome = localArquivoCompleto + "\\" + "Print" + tempo + ".Jpeg";
            if (arquivos.Contains(nome))
            {
                fileName = null;
            }
            else
            {
                fileName = "Print" + tempo;
                nomeArquivoCompleto = string.Format("{0}\\{1}.{2}", localArquivoCompleto, fileName, extensaoArquivo.ToString());
            }
            arquivos.Clear();                        
            
            try
            {
                ((ITakesScreenshot)DriverFactory.INSTANCE)
                    .GetScreenshot()
                    .SaveAsFile(nomeArquivoCompleto, extensaoArquivo);
            }
            catch(Exception ex)
            {
                var mensagemErro = string.Format("{0}\r\n{1}\r\n\r\n{2}",
                    "Ocorreu um erro ao tirar o print da evidência.",
                    ex.Message,
                    ex.StackTrace);

                Console.WriteLine(mensagemErro);
            }

            
            return localArquivoCompleto;
            //return nomeArquivoCompleto;
        }
        public static string TakeScreenshotErro()
        {
            var feature = FeatureContext.Current.FeatureInfo.Title.ToString().Trim().Replace(" ", "");
            var scenario = ScenarioContext.Current.ScenarioInfo.Title.ToString().Trim().Replace(" ", "");
            var step = ScenarioStepContext.Current.StepInfo.Text.ToString().Trim().Replace(" ", "");

            var tempo = string.Format("{0:dd-MM-yyyy-hh-mm-ss}", DateTime.Now);
            var dateTime = GetCurrentDate("dd-MM-yyyy");
            var localArquivoBase = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "Erros");

            localArquivoCompleto = string.Format("{0}\\{1}\\{2}", localArquivoBase, feature, dateTime);

            if (!Directory.Exists(localArquivoCompleto))
            {
                Directory.CreateDirectory(localArquivoCompleto);
            }

            ScreenshotImageFormat extensaoArquivo = ScreenshotImageFormat.Jpeg;

            List<string> arquivos = new List<string>(Directory.GetFiles(localArquivoCompleto));

            var nome = localArquivoCompleto + "\\" + scenario + tempo + ".Jpeg";

            if (arquivos.Contains(nome))
            {
                fileName = null;
            }
            else
            {
                fileName = step + tempo;
                nomeArquivoCompleto = string.Format("{0}\\{1}.{2}", localArquivoCompleto, fileName, extensaoArquivo.ToString());
            }
            arquivos.Clear();

            try
            {
                ((ITakesScreenshot)DriverFactory.INSTANCE)
                    .GetScreenshot()
                    .SaveAsFile(nomeArquivoCompleto, extensaoArquivo);
            }
            catch (Exception ex)
            {
                var mensagemErro = string.Format("{0}\r\n{1}\r\n\r\n{2}",
                    "Ocorreu um erro ao tirar o print da evidência.",
                    ex.Message,
                    ex.StackTrace);

                Console.WriteLine(mensagemErro);
            }
            return nomeArquivoCompleto;
        }


        public static IEnumerable ReadCSVFile(string csvPath)
        {
            using (StreamReader sr = new StreamReader(csvPath, System.Text.Encoding.GetEncoding(1252)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ArrayList result = new ArrayList();
                    result.AddRange(line.Split(';'));
                    yield return result;
                }
            }
        }
    }
}
