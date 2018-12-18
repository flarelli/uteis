using AutomacaoBDD.Helpers;
using AutomacaoBDD.Pages.SMS_API;
using System;
using TechTalk.SpecFlow;

namespace AutomacaoBDD.StepsDefinitions.SimuladorPortabilidade.APISMSPortabilidadeSteps
{
    [Binding]
    public class SimularRespostaSMSSteps : TestBase
    {
        SwaggerRespostaSMSPage _swaggerrespostasmspage = new SwaggerRespostaSMSPage();

        [Given(@"que acesso a tela Swagger API SMS Portabilidade")]
        public void DadoQueAcessoATelaSwaggerAPISMSPortabilidade()
        {
            _swaggerrespostasmspage.AbrirPaginaSwaggerSMS();
        }
        
        [Given(@"acesso envio de resposta SMS")]
        public void DadoAcessoEnvioDeRespostaSMS()
        {
            _swaggerrespostasmspage.ClicarItemGravarRespostaInfobip();
            _swaggerrespostasmspage.ClicarTryItOut();
        }
        
        [When(@"informo os dados do SMS da proposta")]
        public void QuandoInformoOsDadosDoSMSDaProposta(Table table)
        {
            var dictionary = GeneralHelpers.ToDictionary(table);

            _swaggerrespostasmspage.LimparTextArea();
            _swaggerrespostasmspage.InserirTextoTextArea(dictionary["proposta"]);
        }
        
        [When(@"aciono executar")]
        public void QuandoAcionoExecutar()
        {
            _swaggerrespostasmspage.ExecutarResposta();
        }
        
        [Then(@"deve ser registrado a resposta valida para a proposta")]
        public void EntaoDeveSerRegistradoARespostaValidaParaAProposta()
        {
            if (_swaggerrespostasmspage.VerificaRespostaValida() == true)
                Console.WriteLine("Resposta gravada com sucesso");
        }
    }
}
