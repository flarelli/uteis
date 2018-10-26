using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoBDD.Helpers
{
    public static class Utilitarios
    {
        

        public static void HighlightElement(this IWebElement element)
        {
            try
            {
                var jsDriver = (IJavaScriptExecutor)DriverFactory.INSTANCE;
                var ele = element;
                string highlightJavascript = @"$(arguments[0]).css({ ""border-width"" : ""2px"", ""border-style"" : ""solid"", ""border-color"" : ""yellow"" });";
                jsDriver.ExecuteScript(highlightJavascript, new object[] { ele });

            }
            catch
            {
                var jsDriver = (IJavaScriptExecutor)DriverFactory.INSTANCE;
                var ele = element;
                string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: yellow"";";
                jsDriver.ExecuteScript(highlightJavascript, new object[] { ele });
            }

        }


        public static void AddMask(this string value, string parameter)
        {

            if (parameter == "CPF")
            {
                if (value.Length == 14)
                {
                    value = value.Substring(0, 3) + "." +
                   value.Substring(3, 3) + "." +
                   value.Substring(6, 3) + "-" +
                   value.Substring(9, 2);
                }

            }

        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }


        public static string GetFrameworkTestType()
        {
            return ConfigurationManager.AppSettings["TestType"];
        }


        #region Método para verificar download de arquivo

        public static bool CheckFileDownloaded(string filename)
        {
            bool exist = false;
            string Path = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";
            string[] filePaths = Directory.GetFiles(Path);
            foreach (string p in filePaths)
            {
                if (p.Contains(filename))
                {
                    FileInfo thisFile = new FileInfo(p);
                    //Check the file that are downloaded in the last 3 minutes
                    if (thisFile.LastWriteTime.ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
                    thisFile.LastWriteTime.AddMinutes(1).ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
                    thisFile.LastWriteTime.AddMinutes(2).ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
                    thisFile.LastWriteTime.AddMinutes(3).ToShortTimeString() == DateTime.Now.ToShortTimeString())
                        exist = true;
                    File.Delete(p);
                    break;
                }
            }
            return exist;
        }
        #endregion


        public static String CreateDirectoryFolder(string path,string folderName)
        {
            // Specify the directory you want to manipulate.

            try
            {
                if (Directory.Exists(path + "\\" + folderName))
                {
                    Console.WriteLine("That path exists already.");
                    return path + folderName;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path + "\\" + folderName);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path + "\\" + folderName));

                // Delete the directory.
                //di.Delete();

                //Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }

            return path + "\\" + folderName;
        }

    }
}
