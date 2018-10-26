using System.Collections.Generic;
using System.Configuration;
using TechTalk.SpecFlow;
using Castle.DynamicProxy;
using System.Reflection;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using System;
using NUnit.Framework;
using System.IO;
using AutomacaoBDD.Pages.SimuladorPortabilidade;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.MarkupUtils;
using AutomacaoBDD.Functions;

namespace AutomacaoBDD.Helpers
{
    [Binding]
    public class TestBase
    {
        private static List<string> listaErrosCenarios = new List<string>();
        private static int qntCenario = 0;
        private static int qntCenarioSucesso = 0;
        private static double percentagemTestes = 0;
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        public static string TestType = Utilitarios.GetFrameworkTestType();



        private static List<string> listaSteps = new List<string>();
        private static List<string> ListaPrint = new List<string>();

        [BeforeTestRun]
        public static void InitializeReport()
        {
            var dateTime = DateTime.Now.ToString("dd-MM-yyyy");

            var reportPath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "Report");

            if (!Directory.Exists(reportPath + "\\" + dateTime))
            { Directory.CreateDirectory(reportPath + "\\" + dateTime); }


            var tempo = string.Format("{0:dd-MM-yyyy-hh-mm-ss}", DateTime.Now);


            if (!File.Exists(reportPath + "\\" + dateTime + "\\" + "Report " + tempo + ".html"))
            {
                var htmlReporter = new ExtentHtmlReporter(reportPath + "\\" + dateTime + "\\" + "Report " + tempo + ".html");
                htmlReporter.LoadConfig(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "extent-config.xml"));

                extent = new ExtentReports();

                //string css = htmlReporter.Configuration().CSS;
                extent.AttachReporter(htmlReporter);
            }
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
            for (int i=0;i <= ScenarioContext.Current.Count ; i++)
            {
                if(ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "OK")
                    qntCenarioSucesso = qntCenarioSucesso + 1;
            }
                

            if (qntCenario == qntCenarioSucesso)
                SendEmail.EnviarEmail(qntCenario, qntCenarioSucesso, 100);
            else if (qntCenarioSucesso > qntCenario)
                SendEmail.EnviarEmail(qntCenario, qntCenarioSucesso, 0);
            else
            {
                percentagemTestes = (100 * qntCenarioSucesso) / qntCenario;
                SendEmail.EnviarEmail(qntCenario, qntCenarioSucesso, percentagemTestes);
            }
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //se excluir este método, habilitar a linha de criação do featurename no método Initialize
            if (scenario == null)
                featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }
        [AfterStep]
        public void InsertReportinSteps()
        {
            if (!listaSteps.Contains(ScenarioStepContext.Current.StepInfo.Text))
            {
                listaSteps.Add(ScenarioStepContext.Current.StepInfo.Text);

                PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
                MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
                object TestResult = getter.Invoke(ScenarioContext.Current, null);

                var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
                var status = ScenarioContext.Current.ScenarioExecutionStatus.ToString();
                var stepPending = TestResult.ToString();


                if (status == "OK")
                {
                    if (stepType == "Given")
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro());
                    else if (stepType == "When")
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro());
                    else if (stepType == "Then")
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro());
                    else if (stepType == "And")
                        scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro());

                }
                else if (status == "TestError")
                {
                    var erroException = ScenarioContext.Current.TestError.InnerException;

                    if(erroException == null)
                    {
                        if (stepType == "Given")
                            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> STACK TRACE:\r\n {ScenarioContext.Current.TestError.StackTrace} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                        else if (stepType == "When")
                            scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> STACK TRACE:\r\n {ScenarioContext.Current.TestError.StackTrace} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                        else if (stepType == "Then")
                            scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> STACK TRACE:\r\n {ScenarioContext.Current.TestError.StackTrace} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                    }
                    else
                    {
                        if (stepType == "Given")
                            scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> EXCEPTION:\r\n {ScenarioContext.Current.TestError.InnerException} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                        else if (stepType == "When")
                            scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> EXCEPTION:\r\n {ScenarioContext.Current.TestError.InnerException} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                        else if (stepType == "Then")
                            scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Log(Status.Fail, $"<pre style=\"word-wrap: break-word; overflow-x: hidden !important;\"> EXCEPTION:\r\n {ScenarioContext.Current.TestError.InnerException} </pre> => MENSAGEM: <pre> {ScenarioContext.Current.TestError.Message} </pre>").AddScreenCaptureFromPath(GeneralHelpers.TakeScreenshotErro(), "Print");
                    }
                }

                if (stepPending == "StepDefinitionPending")
                {
                    if (stepType == "Given")
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    else if (stepType == "When")
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    else if (stepType == "Then")
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }

                status = null;
                stepPending = null;

            }
            else listaSteps.Clear();

        }

        [BeforeScenario]
        public void Initialize()
        {

            if (DriverFactory.INSTANCE == null)
            {
                DriverFactory.InitializeDriver(ConfigurationManager.AppSettings["BROWSER"]);

                //Metodos para instanciar as Pages no Steps Definitions via injeção de dependência
                this.ProxyGenerator = new ProxyGenerator();
                InjectPageObjects(CollectPageObjects(), null);

                //A linha abaixo pode ser descomentada, se excluir o método BeforeFeature
                //featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
                scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
                qntCenario = qntCenario + 1;
            }
        }

        [AfterScenario]
        public void Close()
        {
            DriverFactory.CloseDriver();
        }

        #region Métodos de injeção de steps

        public static void FailTest(string text)
        {
            scenario.Log(Status.Fail, text);
        }
        public static void FailTestMarkup(String text)
        {
            string txt = text;
            IMarkup Amber = MarkupHelper.CreateLabel(text, ExtentColor.Amber);
            scenario.Log(Status.Fail, Amber);

            IMarkup Black = MarkupHelper.CreateLabel(text, ExtentColor.Black);
            scenario.Log(Status.Fail, Black);

            IMarkup Blue = MarkupHelper.CreateLabel(text, ExtentColor.Blue);
            scenario.Log(Status.Fail, Blue);


            IMarkup Brown = MarkupHelper.CreateLabel(text, ExtentColor.Brown);
            scenario.Log(Status.Fail, Brown);


            IMarkup Cyan = MarkupHelper.CreateLabel(text, ExtentColor.Cyan);
            scenario.Log(Status.Fail, Cyan);

            IMarkup Green = MarkupHelper.CreateLabel(text, ExtentColor.Green);
            scenario.Log(Status.Fail, Green);

            IMarkup Grey = MarkupHelper.CreateLabel(text, ExtentColor.Grey);
            scenario.Log(Status.Fail, Grey);

            IMarkup Indigo = MarkupHelper.CreateLabel(text, ExtentColor.Indigo);
            scenario.Log(Status.Fail, Indigo);

            IMarkup Lime = MarkupHelper.CreateLabel(text, ExtentColor.Lime);
            scenario.Log(Status.Fail, Lime);

            IMarkup Orange = MarkupHelper.CreateLabel(text, ExtentColor.Orange);
            scenario.Log(Status.Fail, Orange);

        }

        public static void InfoTestMarkup(string text)
        {

            String code = " \n\t \n\t\t" + text + "\n\t \n ";

            IMarkup p = MarkupHelper.CreateCodeBlock(code);
            scenario.Log(Status.Info, p);

        }
        private ProxyGenerator ProxyGenerator { get; set; }

        private void InjectPageObjects(List<FieldInfo> fields, IInterceptor proxy)
        {
            foreach (FieldInfo field in fields)
            {
                field.SetValue(this, ProxyGenerator.CreateClassProxy(field.FieldType, proxy));
            }
        }

        private List<FieldInfo> CollectPageObjects()
        {
            List<FieldInfo> fields = new List<FieldInfo>();

            foreach (FieldInfo field in this.GetType().GetRuntimeFields())
            {
                if (field.GetCustomAttribute(typeof(AutoInstance)) != null)
                    fields.Add(field);
            }

            return fields;
        }

        public static void AddSystemInfo()
        {
            extent.AddSystemInfo("User Name", ConfigurationManager.AppSettings["Login"]);
            extent.AddSystemInfo("Autor", ConfigurationManager.AppSettings["Autor"]);
            extent.AddSystemInfo("OS", ConfigurationManager.AppSettings["OS"]);
            extent.AddSystemInfo("Domain", ConfigurationManager.AppSettings["Domain"]);
            extent.AddSystemInfo("Machine Name", ConfigurationManager.AppSettings["MachineName"]);

            extent.AddSystemInfo("Selenium Webdriver Version", "v" + ConfigurationManager.AppSettings["SeleniumVersion"]);

            extent.AddSystemInfo("Extent Report Version", "v" + ConfigurationManager.AppSettings["ExtentReport"]);

            if (TestType == "NunitTests")
            {
                extent.AddSystemInfo("NUnit Version", "v" + ConfigurationManager.AppSettings["NUnitVersion"]);
            }
            else if (TestType == "SpecflowTests")
            {
                extent.AddSystemInfo("Specflow Version", "v" + ConfigurationManager.AppSettings["SpecflowVersion"]);
            }

        }
        #endregion
    }
}
