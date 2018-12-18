using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoBDD.Functions
{
    public class SendEmail
    {
        public static void EnviarEmail(int qntCenario, int qntCenarioSucesso, double percentagemTestes)
        {
            var dateTime = DateTime.Now.ToString("dd-MM-yyyy");
            var reportPath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "Report");

            DirectoryInfo info = new DirectoryInfo(reportPath + "\\" + dateTime);

            FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();

            int totalArquivos = files.Count() - 1;

            var arquivo = reportPath + "\\" + dateTime + "\\" + files[totalArquivos].ToString();


            Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();

            Microsoft.Office.Interop.Outlook.MailItem oMsg = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

            oMsg.HTMLBody = "Relatório de Automação de Testes Mesa Originação" + "<pre>" + "</pre>" + "Ambiente de QA";


            //Adiciona Texto no corpo do e-mail
            String attach = "Attachment to add to the Mail";
            int x = (int)oMsg.Body.Length + 1;
            int y = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;


            //Anexa os arquivos aqui
            Microsoft.Office.Interop.Outlook.Attachment oAttach = oMsg.Attachments.Add(arquivo, y, x, attach);
            //Adiciona Assunto no e-mail
            oMsg.Subject = percentagemTestes + "% - " + qntCenarioSucesso + "/" + qntCenario + " scripts - Mesa Formalização e Originação - Relatório de Testes Automatizados " + dateTime;

            //Informa o e-mail destinatário
            Microsoft.Office.Interop.Outlook.Recipients oRecips = (Microsoft.Office.Interop.Outlook.Recipients)oMsg.Recipients;

            Microsoft.Office.Interop.Outlook.Recipient oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add("flavia.guimaraes@oletecnologia.com.br"); //mesa.originacao@oletecnologia.com.br
            oRecip.Resolve();



            //Envia o e-mail
            oMsg.Send();
        }
    }
}
