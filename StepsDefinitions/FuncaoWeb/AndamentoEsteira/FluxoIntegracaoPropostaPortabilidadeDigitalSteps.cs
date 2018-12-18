using AutomacaoBDD.Helpers;
using AutomacaoBDD.StepsDefinitions.SimuladorPortabilidade.CadastrarPortabilidadeSteps;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace AutomacaoBDD.StepsDefinitions.FuncaoWeb.AndamentoEsteira
{
    [Binding]
    public class FluxoIntegracaoPropostaPortabilidadeDigitalSteps
    {
        CadastrarPortabilidadeSteps _cadastrarportabilidadesteps = new CadastrarPortabilidadeSteps();

        MoveFaseManualSteps _movefasemanualsteps = new MoveFaseManualSteps();

        TestBase _testbase = new TestBase();

        public string proposta;

        [Given(@"que eu crie uma proposta de Portabilidade Digital com os dados")]
        public void DadoQueEuCrieUmaPropostaDePortabilidadeDigitalComOsDados(Table table)
        {
            var dictionary = GeneralHelpers.ToDictionary(table);
            if (dictionary["proposta"] == "Nova Proposta")
            {
                _cadastrarportabilidadesteps.DadoQueAcessoATelaCadastrarPortabilidade();
                _cadastrarportabilidadesteps.DadoQueInformoConvenioClienteEOsDadosDoContratoPortado(table);
                _cadastrarportabilidadesteps.DadoConfirmoQueOTipoDeVendaE("Digital");
                _cadastrarportabilidadesteps.DadoClicoEmProsseguirParaCarregarOsProdutos();
                _cadastrarportabilidadesteps.DadoSelecionoUmProduto();
                _cadastrarportabilidadesteps.DadoClicoEmSimular();
                _cadastrarportabilidadesteps.DadoRealizoASimulacaoComSucesso();
                Thread.Sleep(500);
                _cadastrarportabilidadesteps.DadoProssigoComCadastro();
                _cadastrarportabilidadesteps.QuandoGravarmosAProposta();
                proposta = _cadastrarportabilidadesteps.EntaoDeveSerGeradoONumeroDeProposta();
                //_cadastrarportabilidadesteps.FechaCadastroPortabilidade();
            }
            else
                proposta = dictionary["proposta"];
        }

        [Given(@"movimento a proposta ate a fase '(.*)'")]
        public void DadoMovimentoAPropostaAteAFase(string fase)
        {
            _movefasemanualsteps.DadoQueAcessoOSistemaFuncaoWeb();
            _movefasemanualsteps.DadoAcessoOMenuEsteira_AprovacaoConsulta();
            _movefasemanualsteps.DadoAcionoOBotaoLocalizar();
            //_movefasemanualsteps.DadoInformoAProposta(proposta);
            _movefasemanualsteps.DadoAcionoOPesquisar();
            _movefasemanualsteps.DadoSelecionoAProposta();
            _movefasemanualsteps.DadoSelecionoAFase();
        }
        
        [When(@"movimento a proposta ate a fase '(.*)'")]
        public void QuandoMovimentoAPropostaAteAFase(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"deve ser criado o arquivo do encarteiramento")]
        public void EntaoDeveSerCriadoOArquivoDoEncarteiramento()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"deve ser enviado um email para o responsável pela geração do arquivo")]
        public void EntaoDeveSerEnviadoUmEmailParaOResponsavelPelaGeracaoDoArquivo()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
