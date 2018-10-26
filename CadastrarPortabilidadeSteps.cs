using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AutomacaoBDD.Helpers;
using AutomacaoBDD.Functions;
using AutomacaoBDD.Pages.SimuladorPortabilidade;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

using System.Configuration;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using AutomacaoBDD.Functions.TaxaPonderada;

namespace AutomacaoBDD.StepsDefinitions.SimuladorPortabilidade.CadastrarPortabilidadeSteps
{
    [Binding]
    public class CadastrarPortabilidadeSteps : TestBase
    {
        [AutoInstance]
        CadastrarPortabilidadePage _cadastrarportabilidadePage;

        [AutoInstance]
        AnexarDocumentacaoPage _anexardocumentacaoPage;

        [Given(@"que acesso a tela Cadastrar Portabilidade")]
        public void DadoQueAcessoATelaCadastrarPortabilidade()
        {
            _cadastrarportabilidadePage.AbrirPaginaCadastroPortabilidade();
        }

        [Given(@"que exibem somente convenios que permitem Portabilidade")]
        public void DadoQueExibemSomenteConveniosQuePermitemPortabilidade()
        {
            List<string> listaOpcoesCbxConvenio = new List<string>(PageBase.GetAllOptionsComboValue(By.Id("ddlConvenio")));
            listaOpcoesCbxConvenio.RemoveAt(0);

            List<List<string>> conveniosPortabilidade = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.conveniosQuePermitemPortabilidade);

            foreach (var c in conveniosPortabilidade)
            {
                if (listaOpcoesCbxConvenio.Contains(c[0]) == true)
                    Console.WriteLine(c[0] + " Convenio correto");
            }
        }

        [Given(@"que informo convenio cliente e os dados do contrato portado")]
        public void DadoQueInformoConvenioClienteEOsDadosDoContratoPortado(Table table)
        {
            var dictionary = GeneralHelpers.ToDictionary(table);

            _cadastrarportabilidadePage.PreencherConvenio(dictionary["convenio"]);
            _cadastrarportabilidadePage.PreencherNome("AUTOMACAO - Mesa originacao");
            _cadastrarportabilidadePage.PreencherCPF(dictionary["cpf"]);
            _cadastrarportabilidadePage.PreencherDataNascimento("02041987");
            _cadastrarportabilidadePage.PreencherBancoPortabilidade("001");
            _cadastrarportabilidadePage.PreencherNumContrato("Randon");
            _cadastrarportabilidadePage.PreencherQntParcelas(dictionary["qtdparcelas"]);
            _cadastrarportabilidadePage.PreencherVlrParcela(dictionary["vlrparcela"]);
            _cadastrarportabilidadePage.ClicarRadionButtonTaxa();
            _cadastrarportabilidadePage.PreencherTaxaDoContratoPortado(dictionary["taxaPortada"]);
            _cadastrarportabilidadePage.PreencherTotalParcProduto(dictionary["totalparcelas"]);
        }

        [Given(@"clico em prosseguir para carregar os produtos")]
        public void DadoClicoEmProsseguirParaCarregarOsProdutos()
        {
            _cadastrarportabilidadePage.ClicarProsseguirCarregaProdutos();
        }
        [When(@"clico em prosseguir para carregar os produtos")]
        public void QuandoClicoEmProsseguirParaCarregarOsProdutos()
        {
            _cadastrarportabilidadePage.ClicarProsseguirCarregaProdutos();
        }

        [Given(@"seleciono um produto")]
        public void DadoSelecionoUmProduto()
        {
            _cadastrarportabilidadePage.SelecionarProduto();            
        }

        [Given(@"realizo a simulacao com sucesso")]
        public void DadoRealizoASimulacaoComSucesso()
        {
            _cadastrarportabilidadePage.ClicarGravarSimulacao();
        }
        [When(@"seleciono um produto")]
        public void QuandoSelecionoUmProduto()
        {
            _cadastrarportabilidadePage.SelecionarProduto();
        }

        [Given(@"clico em simular")]
        public void DadoClicoEmSimular()
        {
            _cadastrarportabilidadePage.ClicarSimular();
        }

        [When(@"clico em simular")]
        public void QuandoClicoEmSimular()
        {
            _cadastrarportabilidadePage.ClicarSimular();
        }

        [Given(@"deve ser exibida a Taxa Ponderada Calculada")]
        public void DadoDeveSerExibidaATaxaPonderadaCalculada()
        {
            var taxacalculada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

            var taxapreenchida = _cadastrarportabilidadePage.RetornaTaxaPonderada().Replace(".",",");

            if (taxapreenchida.Equals(taxacalculada))
                Console.WriteLine("Taxa Calculada com sucesso!");
        }

        [Then(@"deve ser exibida a Taxa Ponderada Calculada")]
        public void EntaoDeveSerExibidaATaxaPonderadaCalculada()
        {
            var taxacalculada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

            var taxapreenchida = _cadastrarportabilidadePage.RetornaTaxaPonderada();

            if (taxapreenchida.Equals(taxacalculada))
                Console.WriteLine("Taxa Calculada com sucesso!");
        }

        [Given(@"prossigo com cadastro")]
        public void DadoProssigoComCadastro()
        {
            _cadastrarportabilidadePage.PreencherUsuarioPortabilidade("RECOMPRA");
            _cadastrarportabilidadePage.PreencherSenhaPortabilidade(ConfigurationManager.AppSettings["SenhaFuncaoRecompra"]);
            _cadastrarportabilidadePage.ClicarEntrar();
            _cadastrarportabilidadePage.SelecionarLojaAleatoria();
            _cadastrarportabilidadePage.SelecionarOrgaoAleatorio();
            _cadastrarportabilidadePage.PreencherCodigoBeneficio();
            _cadastrarportabilidadePage.PreencherVersaoFormulario("EMP006");
            _cadastrarportabilidadePage.PreencherRenda("5000,00");
            _cadastrarportabilidadePage.ClicarProsseguir();
            _cadastrarportabilidadePage.SelecionarNacionalidade("B");
            _cadastrarportabilidadePage.PreencherNaturalidade("Belo Horizonte");
            _cadastrarportabilidadePage.SelecionarSexo("F");
            _cadastrarportabilidadePage.SelecionarEstadoCivil("SO");
            _cadastrarportabilidadePage.PreencherRG("MG15487899");
            _cadastrarportabilidadePage.PreencherUfRg("MG");
            _cadastrarportabilidadePage.PreencherOrgaoEmissor("sspmg");
            _cadastrarportabilidadePage.PreencherDataEmissao("02022002");
            _cadastrarportabilidadePage.PreencherCep("30570080");
            _cadastrarportabilidadePage.PreencherNumero("133");
            _cadastrarportabilidadePage.PreencherDDDTel("31");
            _cadastrarportabilidadePage.PreencherNumeroTel("Randon");

            DatabaseFactory.DBExecutarQuery(Scripts.tornaCelularInformadoViávelPraVendaDigital.Replace("@CelularDigital", ConfigurationManager.AppSettings["CelularDigital"]));

            _cadastrarportabilidadePage.PreencherDDDCel("31");
            _cadastrarportabilidadePage.PreencherNumeroCel(ConfigurationManager.AppSettings["CelularDigital"]);

            Funções.CriarPrintEvidencias(CadastrarPortabilidadePage.numProposta, "Evidências");

            _cadastrarportabilidadePage.PreencherMatricula("Randon");
            _cadastrarportabilidadePage.PreencherNomePai("AUTOMAÇÃO");
            _cadastrarportabilidadePage.PreencherNomeMae("AUTOMAÇÃO");

            _cadastrarportabilidadePage.PreencherEmail("deinf@oleconsignado.com.br");

            _cadastrarportabilidadePage.SelecionarTipoContaCliente("01");
            _cadastrarportabilidadePage.PreencherBancoCliente("001");
            _cadastrarportabilidadePage.PreencherAgenciaCliente("0250");
            _cadastrarportabilidadePage.PreencherDigitoAgenciaCliente("0");
            _cadastrarportabilidadePage.PreencherContaCliente("124666");
            _cadastrarportabilidadePage.PreencherDigitoContaCliente("2");

            _cadastrarportabilidadePage.ClicarBtnProsseguirDados();

            _cadastrarportabilidadePage.PreencherTipoContaTerceiro("01");
            _cadastrarportabilidadePage.PreencherBancoTerceiro("001");
            _cadastrarportabilidadePage.PreencherAgenciaTerceiro("1957");
            _cadastrarportabilidadePage.PreencherDigitoAgenciaTerceiro("0");
            _cadastrarportabilidadePage.PreencherContaTerceiro("13256");
            _cadastrarportabilidadePage.PreencherDigitoContaTerceiro("6");

            _cadastrarportabilidadePage.PreencherCodAverbacao("31213645");
            _cadastrarportabilidadePage.PreencherCodAutenticacao("2121121");

            _cadastrarportabilidadePage.ClicarProsseguirObservacao();
        }

        [When(@"gravarmos a proposta")]
        public void QuandoGravarmosAProposta()
        {
            _cadastrarportabilidadePage.ClicarProsseguirFinal();
        }

        [Then(@"deve ser gerado o numero de Proposta")]
        public void EntaoDeveSerGeradoONumeroDeProposta()
        {
            _cadastrarportabilidadePage.GeraçãoNumeroProposta();
            Funções.CriarPrintEvidencias(CadastrarPortabilidadePage.numProposta, "Evidências");
            DatabaseFactory.DBExecutarQuery(Scripts.ligaAPITaxaPonderada);
        }

        [Then(@"realizo a anexacao dos documentos")]
        public void EntaoRealizoAAnexacaoDosDocumentos()
        {
            List<string> faseProposta = new List<string>(Funções.RetornaFaseProposta(CadastrarPortabilidadePage.numProposta));
            if (faseProposta[0] == "702")
            {
                _cadastrarportabilidadePage.ClicarAnexarNaPortabilidade();
                _anexardocumentacaoPage.AnexaDocumento();
                _anexardocumentacaoPage.ClicarEmAprovarAnexação();
                Funções.CriarPrintEvidencias(CadastrarPortabilidadePage.numProposta, "Evidências");
            }
            else Console.WriteLine("Proposta não está na fase de anexação");
        }

        [Given(@"confirmo que o Tipo de Venda e '(.*)'")]
        public void DadoConfirmoQueOTipoDeVendaE(string tipoVenda)
        {
            _cadastrarportabilidadePage.ClicarTipoVenda(tipoVenda);
        }

        [Given(@"taxa ponderada esta habilitada")]
        public void DadoTaxaPonderadaEstaHabilitada()
        {
            DatabaseFactory.DBExecutarQuery(Scripts.ligaAPITaxaPonderada);
        }
        [Given(@"taxa ponderada esta desabilitada")]
        public void DadoTaxaPonderadaEstaDesabilitada()
        {
            DatabaseFactory.DBExecutarQuery(Scripts.desligaAPITaxaPonderada);
        }
        [When(@"taxa ponderada esta desabilitada")]
        public void QuandoTaxaPonderadaEstaDesabilitada()
        {
            DatabaseFactory.DBExecutarQuery(Scripts.desligaAPITaxaPonderada);
        }

        [Given(@"log de chamada da API de Calculo de Taxa Ponderada e gravado")]
        public void DadoLogDeChamadaDaAPIDeCalculoDeTaxaPonderadaEGravado()
        {
            if (TaxaPonderada.VerificaLogChamadaAPITaxaPonderada() == true)
                Console.WriteLine("Log da chamada registrado com sucesso!");
        }
        [When(@"log de chamada da API de Calculo de Taxa Ponderada e gravado")]
        public void QuandoLogDeChamadaDaAPIDeCalculoDeTaxaPonderadaEGravado()
        {
            if (TaxaPonderada.VerificaLogChamadaAPITaxaPonderada() == true)
                Console.WriteLine("Log da chamada registrado com sucesso!");
        }

        [Given(@"devem ser listados todos os produtos '(.*)' de COMP do convenio selecionado com taxa menor ou igual a taxa ponderada")]
        public void DadoDevemSerListadosTodosOsProdutosDeCOMPDoConvenioSelecionadoComTaxaMenorOuIgualATaxaPonderada(string tipoVenda)
        {
            if (tipoVenda == "Padrao")
            {
                string taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida,CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado).ToString();

                var query = Scripts.produtosPadraoComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada);
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query.Replace(",","."));
                                
                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
            else if (tipoVenda == "Digital")
            {
                var taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

                var query = Scripts.produtosDigitalComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada.ToString());
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query.Replace(",","."));

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
        }

        [Then(@"devem ser listados todos os produtos '(.*)' de COMP do convenio selecionado com taxa menor ou igual a taxa ponderada")]
        public void EntaoDevemSerListadosTodosOsProdutosDeCOMPDoConvenioSelecionadoComTaxaMenorOuIgualATaxaPonderada(string tipoVenda)
        {
            if (tipoVenda == "Padrao")
            {
                var taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

                var query = Scripts.produtosPadraoComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada.ToString());
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query);

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
            else if (tipoVenda == "Digital")
            {
                var taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

                var query = Scripts.produtosDigitalComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada.ToString());
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query);

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
        }

        [Given(@"devem ser listados todos os produtos '(.*)' de COMP do convenio selecionado")]
        public void DadoDevemSerListadosTodosOsProdutosDeCOMPDoConvenioSelecionado(string tipoVenda)
        {
            if (tipoVenda == "Padrao")
            {
                var taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

                var query = Scripts.produtosPadraoComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada.ToString());
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query);

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
            else if (tipoVenda == "Digital")
            {
                var taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

                var query = Scripts.produtosDigitalComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada.ToString());
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query);

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
        }

        [Then(@"devem ser listados todos os produtos '(.*)' de COMP do convenio selecionado")]
        public void EntaoDevemSerListadosTodosOsProdutosDeCOMPDoConvenioSelecionado(string tipoVenda)
        {
            if (tipoVenda == "Padrao")
            {

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                var query = Scripts.produtosPadraoCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado);
                List<List<string>> retornaProdutosPadraoCOMP = DatabaseFactory.DBRetornarListaDadosQuery(query);

                foreach (var c in retornaProdutosPadraoCOMP)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]))
                        Console.WriteLine("Sucesso na listagem de produtos");
                }
            }
            else if (tipoVenda == "Digital")
            {
                var query = Scripts.produtosDigitalCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado);
                List<List<string>> retornaProdutosDigitaisCOMP = DatabaseFactory.DBRetornarListaDadosQuery(query);

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in retornaProdutosDigitaisCOMP)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]))
                        Console.WriteLine("Sucesso na listagem de produtos");
                }
            }
        }

        [Given(@"gravarmos a proposta")]
        public void DadoGravarmosAProposta()
        {
            _cadastrarportabilidadePage.ClicarProsseguirFinal();
        }

        [When(@"preencho o campo email")]
        public void QuandoPreenchoOCampoEmail(Table table)
        {
            var dictionary = GeneralHelpers.ToDictionary(table);

            _cadastrarportabilidadePage.PreencherUsuarioPortabilidade("RECOMPRA");
            _cadastrarportabilidadePage.PreencherSenhaPortabilidade(ConfigurationManager.AppSettings["SenhaFuncaoRecompra"]);
            _cadastrarportabilidadePage.ClicarEntrar();
            _cadastrarportabilidadePage.SelecionarLojaAleatoria();
            _cadastrarportabilidadePage.SelecionarOrgaoAleatorio();
            _cadastrarportabilidadePage.PreencherCodigoBeneficio();
            _cadastrarportabilidadePage.PreencherVersaoFormulario("EMP006");
            _cadastrarportabilidadePage.PreencherRenda("5000,00");
            _cadastrarportabilidadePage.ClicarProsseguir();

            _cadastrarportabilidadePage.SelecionarNacionalidade("B");
            _cadastrarportabilidadePage.PreencherNaturalidade("Belo Horizonte");
            _cadastrarportabilidadePage.SelecionarSexo("F");
            _cadastrarportabilidadePage.SelecionarEstadoCivil("SO");
            _cadastrarportabilidadePage.PreencherRG("MG15487899");
            _cadastrarportabilidadePage.PreencherUfRg("MG");
            _cadastrarportabilidadePage.PreencherOrgaoEmissor("sspmg");
            _cadastrarportabilidadePage.PreencherDataEmissao("02022002");
            _cadastrarportabilidadePage.PreencherCep("30570080");
            _cadastrarportabilidadePage.PreencherNumero("133");
            _cadastrarportabilidadePage.PreencherDDDTel("31");
            _cadastrarportabilidadePage.PreencherNumeroTel("Randon");
            _cadastrarportabilidadePage.PreencherDDDCel("31");
            _cadastrarportabilidadePage.PreencherNumeroCel("Randon");

            Functions.Funções.CriarPrintEvidencias(CadastrarPortabilidadePage.numProposta, "Evidências");

            _cadastrarportabilidadePage.PreencherMatricula("Randon");
            _cadastrarportabilidadePage.PreencherNomePai("AUTOMAÇÃO");
            _cadastrarportabilidadePage.PreencherNomeMae("AUTOMAÇÃO");

            _cadastrarportabilidadePage.PreencherEmail(dictionary["email_invalido"]);

            _cadastrarportabilidadePage.SelecionarTipoContaCliente("01");
            _cadastrarportabilidadePage.PreencherBancoCliente("001");
            _cadastrarportabilidadePage.PreencherAgenciaCliente("0250");
            _cadastrarportabilidadePage.PreencherDigitoAgenciaCliente("0");
            _cadastrarportabilidadePage.PreencherContaCliente("124666");
            _cadastrarportabilidadePage.PreencherDigitoContaCliente("2");
        }

        [When(@"prossigo com cadastro")]
        public void QuandoProssigoComCadastro()
        {
            _cadastrarportabilidadePage.ClicarBtnProsseguirDados();
        }

        [Then(@"deve ser exibida uma mensagem de e-mail incorreto")]
        public void EntaoDeveSerExibidaUmaMensagemDeE_MailIncorreto()
        {
            if (_cadastrarportabilidadePage.ValidaMsgEmailInvalido() == true)
            {
                Console.WriteLine("Validação de email inválido feita corretamente!");
            }
        }

        [When(@"o CPF informado possui restricao")]
        public void QuandoOCPFInformadoPossuiRestricao()
        {
            var query = Scripts.verificarCpfNegativado.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente);
            List<List<string>> resultado = DatabaseFactory.DBRetornarListaDadosQuery(query);

            
            if (resultado[0] == null)//se não tem dado na tabela, não tem restrição, então inserimos
                DatabaseFactory.DBExecutarQuery((Scripts.insereCpfNaListaDeNegativados).Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));
        }
        [When(@"o CPF informado possui restricao por '(.*)'")]
        public void QuandoOCPFInformadoPossuiRestricaoPor(string tipoRestricao)
        {
            if (tipoRestricao == "Negativacao")
            {
                List<List<string>> resultado = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.verificarCpfNegativado.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));

                if (resultado == null || resultado.Count == 0)                    
                    DatabaseFactory.DBExecutarQuery((Scripts.insereCpfNaListaDeNegativados.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente)));

            }
            else if (tipoRestricao == "semRepasse")
            {
                List<List<string>> resultado = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.VerificaCpfSemRepasseMais45Dias.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));
                
                if (resultado == null || resultado.Count == 0)
                    DatabaseFactory.DBExecutarQuery((Scripts.insereCpfRemRepasseMais45Dias).Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));
            }
        }

        [Given(@"CPF passou na validacao da lista restritiva")]
        public void DadoCPFPassouNaValidacaoDaListaRestritiva()
        {
            List<List<string>> listanegativado = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.verificarCpfNegativado.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));
            List<List<string>> listasemrepasse = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.VerificaCpfSemRepasseMais45Dias.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));

            if (listanegativado != null || listanegativado.Count > 0)
                DatabaseFactory.DBExecutarQuery(Scripts.CpfNaoNegativado.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));
            if (listasemrepasse != null || listasemrepasse.Count > 0)
                DatabaseFactory.DBExecutarQuery(Scripts.cpfSemProblemaRepasse.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));
        }

        [Then(@"lista de produtos disponiveis fica vazia")]
        public void EntaoListaDeProdutosDisponiveisFicaVazia()
        {
            List<string> lista = new List<string>(_cadastrarportabilidadePage.ListaDeProdutos());
            if (lista[0].ToString() == "Não existem produtos disponíveis nestas condições")
                Console.WriteLine("Lista realmente esta vazia!");
        }


        [Given(@"prossigo o cadastro da portabilidade ate o cadastro do cliente")]
        public void QuandoProssigoOCadastroDaPortabilidadeAteOCadastroDoCliente()
        {
            _cadastrarportabilidadePage.PreencherUsuarioPortabilidade("RECOMPRA");
            _cadastrarportabilidadePage.PreencherSenhaPortabilidade(ConfigurationManager.AppSettings["SenhaFuncaoRecompra"]);
            _cadastrarportabilidadePage.ClicarEntrar();
            _cadastrarportabilidadePage.SelecionarLojaAleatoria();
            _cadastrarportabilidadePage.SelecionarOrgaoAleatorio();
            _cadastrarportabilidadePage.PreencherCodigoBeneficio();
            _cadastrarportabilidadePage.PreencherVersaoFormulario("EMP006");
            _cadastrarportabilidadePage.PreencherRenda("5000,00");
            _cadastrarportabilidadePage.ClicarProsseguir();

            _cadastrarportabilidadePage.SelecionarNacionalidade("B");
            _cadastrarportabilidadePage.PreencherNaturalidade("Belo Horizonte");
            _cadastrarportabilidadePage.SelecionarSexo("F");
            _cadastrarportabilidadePage.SelecionarEstadoCivil("SO");
            _cadastrarportabilidadePage.PreencherRG("MG15487899");
            _cadastrarportabilidadePage.PreencherUfRg("MG");
            _cadastrarportabilidadePage.PreencherOrgaoEmissor("sspmg");
            _cadastrarportabilidadePage.PreencherDataEmissao("02022002");
            _cadastrarportabilidadePage.PreencherCep("30570080");
            _cadastrarportabilidadePage.PreencherNumero("133");
            _cadastrarportabilidadePage.PreencherDDDTel("31");
            _cadastrarportabilidadePage.PreencherNumeroTel("Randon");
            _cadastrarportabilidadePage.PreencherDDDCel("31");
            _cadastrarportabilidadePage.PreencherNumeroCel("Randon");

            Functions.Funções.CriarPrintEvidencias(CadastrarPortabilidadePage.numProposta, "Evidências");

            _cadastrarportabilidadePage.PreencherMatricula("Randon");
            _cadastrarportabilidadePage.PreencherNomePai("AUTOMAÇÃO");
            _cadastrarportabilidadePage.PreencherNomeMae("AUTOMAÇÃO");

            _cadastrarportabilidadePage.PreencherEmail("mesa.originacao@oletecnologia.com.br");

            _cadastrarportabilidadePage.SelecionarTipoContaCliente("01");
            _cadastrarportabilidadePage.PreencherBancoCliente("001");
            _cadastrarportabilidadePage.PreencherAgenciaCliente("0250");
            _cadastrarportabilidadePage.PreencherDigitoAgenciaCliente("0");
            _cadastrarportabilidadePage.PreencherContaCliente("124666");
            _cadastrarportabilidadePage.PreencherDigitoContaCliente("2");

            _cadastrarportabilidadePage.ClicarBtnProsseguirDados();
        }
    }
}
