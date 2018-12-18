using AutomacaoBDD.Helpers;
using AutomacaoBDD.Pages.FuncaoWeb.AndamentoEsteira;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;

namespace AutomacaoBDD.StepsDefinitions.FuncaoWeb.AndamentoEsteira
{
    [Binding]
    public class MoveFaseManualSteps : TestBase
    {
        FuncaoWebLoginPage _funcaowebloginpage = new FuncaoWebLoginPage();

        AprovacaoConsultaPage _aprovacaoconsultapage = new AprovacaoConsultaPage();

        string faseparaatuacao;

        [Given(@"que acesso o sistema Função Web")]
        public void DadoQueAcessoOSistemaFuncaoWeb()
        {
            _funcaowebloginpage.AbrirPaginaFuncaoWeb();
            _funcaowebloginpage.PreencherLogin("ADMINFUNCAO");
            _funcaowebloginpage.PreencherSenha("ambqa001@");
            _funcaowebloginpage.ClicarEntrar();
        }
        
        [Given(@"acesso o menu Esteira - Aprovação / Consulta")]
        public void DadoAcessoOMenuEsteira_AprovacaoConsulta()
        {
            _aprovacaoconsultapage.AcessaMenuEsteira();
            _aprovacaoconsultapage.AcessaSubMenuAprovacaoConsulta();
        }
        
        [Given(@"aciono o botão Localizar")]
        public void DadoAcionoOBotaoLocalizar()
        {
            _aprovacaoconsultapage.ClicarLocalizar();
        }
        
        [Given(@"informo a proposta")]
        public void DadoInformoAProposta(Table table)
        {
            var dictionary = GeneralHelpers.ToDictionary(table);
            _aprovacaoconsultapage.EscolherChavePesquisa("Proposta");
            _aprovacaoconsultapage.PreencherDescPesquisaProposta(dictionary["proposta"]);
        }
        
        [Given(@"aciono o Pesquisar")]
        public void DadoAcionoOPesquisar()
        {
            _aprovacaoconsultapage.ClicarPesquisarProposta();
        }
        
        [Given(@"seleciono a proposta")]
        public void DadoSelecionoAProposta()
        {
            _aprovacaoconsultapage.ClicarPropostaEncontrada();
        }
        
        [Given(@"seleciono a fase")]
        public void DadoSelecionoAFase()
        {            
            List<List<string>> faseatualproposta = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.retornafaseatual.Replace("@proposta", AprovacaoConsultaPage.propostainformada));
            _aprovacaoconsultapage.ClicarProposta(faseatualproposta[0][0]);           
        }

        [When(@"esta na fase")]
        public void QuandoEstaNaFase(Table table)
        {
            var dictionary = GeneralHelpers.ToDictionary(table);

            faseparaatuacao = dictionary["fase"];
        }
            

        [When(@"marco a fase")]
        public void QuandoMarcoAFase()
        {
            List<List<string>> faseatualproposta = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.retornafaseatual.Replace("@proposta", AprovacaoConsultaPage.propostainformada));
            _aprovacaoconsultapage.MarcarAFase(faseatualproposta[0][0]);
            Thread.Sleep(1000);
        }

        [When(@"aciono a aprovação")]
        public void QuandoAcionoAAprovacao()
        {
            foreach (var c in faseparaatuacao)
            {
                List<List<string>> faseatualproposta = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.retornafaseatual.Replace("@proposta", AprovacaoConsultaPage.propostainformada));

                if (faseparaatuacao != faseatualproposta[0][0])
                {                    
                    if(_aprovacaoconsultapage.BotaoAprovacaoEstaHabilitado() == true)
                    {
                        _aprovacaoconsultapage.AprovaFase();
                        Thread.Sleep(1000);
                        if (_aprovacaoconsultapage.FaseDetalhadaEstaHabilitada() == true)
                            _aprovacaoconsultapage.AprovaFaseDetalhada();
                    }
                        
                    else
                    {
                        List<List<string>> faseatualproposta1 = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.retornafaseatual.Replace("@proposta", AprovacaoConsultaPage.propostainformada));
                        if (faseatualproposta1[0][0] == "ANEXAR DOCUMENTO" || faseatualproposta1[0][0] == "ANEXA DOCUMENTACAO")
                            _aprovacaoconsultapage.EnviaDocumentacao();
                        else if(_aprovacaoconsultapage.FaseDetalhadaEstaHabilitada() == true)
                            _aprovacaoconsultapage.AprovaFaseDetalhada();
                    }                    
                }
            }
        }

        [Then(@"deve ser movida de fase")]
        public void EntaoDeveSerMovidaDeFase()
        {
            List<List<string>> faseatualproposta = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.retornafaseatual.Replace("@proposta", AprovacaoConsultaPage.propostainformada));
            if (faseparaatuacao == faseatualproposta[0][0])
                Console.WriteLine("Proposta foi movida da fase " + faseatualproposta[0][0]);
        }
        [Then(@"deve sair do função")]
        public void EntaoDeveSairDoFuncao()
        {
            _aprovacaoconsultapage.SairDaProposta();
        }

    }
}
