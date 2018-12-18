using AutomacaoBDD.Functions;
using AutomacaoBDD.Functions.GeradorDeDadosTestes;
using AutomacaoBDD.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace AutomacaoBDD.Pages.SimuladorPortabilidade
{
    public class CadastrarPortabilidadePage : PageBase
    {
        #region Mapeamento dos elementos da tela

        #region Mapeamento elementos de Dados de Inicio da Simulação
        
        IWebElement txtCodConvenio => DriverFactory.INSTANCE.FindElement(By.Id("inputConvenio"));
        
        IWebElement btnConvenio => DriverFactory.INSTANCE.FindElement(By.XPath("//*[@id='pnlConteudo']/p[1]/span[1]/a/span[1]"));

        IWebElement txtCodProduto => DriverFactory.INSTANCE.FindElement(By.Id("txtCodProduto"));
                
        IWebElement txtNome => DriverFactory.INSTANCE.FindElement(By.Id("txtNome"));
                
        IWebElement txtCPF => DriverFactory.INSTANCE.FindElement(By.Id("txtCPF"));
        
        IWebElement txtDataNascimento => DriverFactory.INSTANCE.FindElement(By.Id("txtDataNascimento"));
                
        IWebElement txtBancoPortabilidade => DriverFactory.INSTANCE.FindElement(By.Id("txtBanco"));
                
        IWebElement txtNumeroContrato => DriverFactory.INSTANCE.FindElement(By.Id("txtNumeroContrato"));
                
        IWebElement txtQuantidadeParcelas => DriverFactory.INSTANCE.FindElement(By.Id("txtQuantidadeParcelas"));

        IWebElement txtValorParcelas => DriverFactory.INSTANCE.FindElement(By.Id("txtValorParcelas"));

        IWebElement rbTaxa => DriverFactory.INSTANCE.FindElement(By.Id("rbTaxa"));

        IWebElement txtTaxaContratoPortado => DriverFactory.INSTANCE.FindElement(By.Id("txtTaxaContrato"));
        
        IWebElement txtQtdTotalParcelas => DriverFactory.INSTANCE.FindElement(By.Id("txtQtdTotalParcelas"));

        IWebElement btnProsseguirPrd => DriverFactory.INSTANCE.FindElement(By.Id("btnProsseguir"));

        IWebElement btnSimular => DriverFactory.INSTANCE.FindElement(By.Id("btnSimular"));
                
        IWebElement cbxProduto => DriverFactory.INSTANCE.FindElement(By.Id("ddlProduto"));
                
        IWebElement taxaProduto => DriverFactory.INSTANCE.FindElement(By.Id("txtTaxaProduto"));

        IWebElement rbTipoVenda => DriverFactory.INSTANCE.FindElement(By.Id("rblTipoVenda"));

        IWebElement rbTipoVendaDigital => DriverFactory.INSTANCE.FindElement(By.Id("rblTipoVenda_0"));

        IWebElement rbTipoVendaPadrao => DriverFactory.INSTANCE.FindElement(By.Id("rblTipoVenda_1"));

        IWebElement btnSim => DriverFactory.INSTANCE.FindElement(By.Id("btnContinuarVendaPadraoSim"));

        IWebElement msgValidacaoInicioSim => DriverFactory.INSTANCE.FindElement(By.Id("vdsPagina"));
        #endregion

        #region Mapeamento elementos da Simulação

        IWebElement saldodevedor => DriverFactory.INSTANCE.FindElement(By.Id("txtResRefSaldoDevedor"));

        IWebElement parcelasabertos => DriverFactory.INSTANCE.FindElement(By.Id("txtResRefParcelasAberto"));
        
        IWebElement vlrparcelas => DriverFactory.INSTANCE.FindElement(By.Id("txtResRefValorParcelas"));
        
        IWebElement vlrcontrato => DriverFactory.INSTANCE.FindElement(By.Id("txtResRefValorContrato"));
        
        IWebElement vlrliberado => DriverFactory.INSTANCE.FindElement(By.Id("txtResRefValorLiberado"));

        IWebElement vlrcustooriginacao => DriverFactory.INSTANCE.FindElement(By.Id("txtResRefCustoOriginacao"));
                
        IWebElement vlrtaxaponderada => DriverFactory.INSTANCE.FindElement(By.Id("txtDtlTaxaPonderada"));
        
        IWebElement motivosimulacao => DriverFactory.INSTANCE.FindElement(By.Id("lblMotivo"));

        IWebElement btnGravarSimulacao => DriverFactory.INSTANCE.FindElement(By.Id("btnSalvar"));

        IWebElement btnEditarSimulacao => DriverFactory.INSTANCE.FindElement(By.Id("btnEditar"));


        #endregion

        #region Mapeamento dos elementos de Login Portabilidade

        IWebElement txtUsuario => DriverFactory.INSTANCE.FindElement(By.Id("txtUsuario"));

        [FindsBy(How = How.Id, Using = "")]
        IWebElement txtSenha => DriverFactory.INSTANCE.FindElement(By.Id("txtSenha"));

        [FindsBy(How = How.Id, Using = "")]
        IWebElement btnEntrar => DriverFactory.INSTANCE.FindElement(By.Id("btnEntrar"));

        #endregion

        #region Mapeamento dos elementos de Dados Principais da Proposta

        IWebElement txtProdPort => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosIniciais_ucAccordion1_ddlLoja"));

        IWebElement cbxLoja => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosIniciais_ucAccordion1_ddlLoja"));

        IWebElement cbxOrgao => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosIniciais_ucAccordion1_ddlOrgao"));

        IWebElement txtCodBeneficio => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosIniciais_ucAccordion1_txtCodBeneficio"));

        IWebElement txtVersaoFormulario => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosIniciais_ucAccordion1_txtVersaoFormulario"));

        IWebElement txtRenda => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosIniciais_ucAccordion1_txtRenda"));

        IWebElement btnProsseguir => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosIniciais_ucAccordion1_btnProsseguir"));

        IWebElement btnVoltar => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosIniciais_ucAccordion1_btnVoltar"));

        #endregion

        #region Mapeamento dos elementos de Cadastro do Cliente

        IWebElement txtnomecliente => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtNome"));

        IWebElement txtDataNascimentoCliente => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtDataNascimento"));

        IWebElement cbxNacionalidade => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ddlNacionalidade"));

        IWebElement txtNaturalidade => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtNaturalidade"));

        IWebElement cbxSexo => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ddlSexo"));

        IWebElement cbxEstadoCivil => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ddlEstadoCivil"));

        IWebElement txtRG => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtRG"));

        IWebElement cbxUFRG => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ddlUfRg"));

        IWebElement txtOrgaoEmissor => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtOrgaoEmissor"));

        IWebElement txtDataEmissao => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtDataEmissao"));

        IWebElement txtCEP => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ucEnderecoCliente_txtCep"));

        IWebElement cbxUF => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ucEnderecoCliente_ddlUf"));

        IWebElement txtEndereco => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ucEnderecoCliente_txtEndereco"));

        IWebElement txtNumero => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ucEnderecoCliente_txtNumero"));

        IWebElement txtBairro => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ucEnderecoCliente_txtBairro"));

        IWebElement txtCidade => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ucEnderecoCliente_txtCidade"));

        IWebElement txtDDDTel => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ucTelefoneCliente_txtDDD"));

        IWebElement txtNumeroTelefone => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ucTelefoneCliente_txtNumeroTelefone"));

        IWebElement txtDDDCel => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ucTelefoneCelularDadosAdicionais_txtDDD"));

        IWebElement txtNumeroCelular => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_ucTelefoneCelularDadosAdicionais_txtNumeroTelefone"));

        IWebElement txtMatricula => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtMatricula"));

        IWebElement txtNomePai => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtNomePai"));

        IWebElement txtNomeMae => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtNomeMae"));

        IWebElement txtEmail => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtEmail"));

        IWebElement msgValidacao => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosCliente_vdsDadosCliente"));

        #endregion

        #region Mapeamento dos elementos dos Dados Bancarios do Cliente

        IWebElement cbxTipoConta => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosBancarios_ddlTipoConta"));

        IWebElement txtBanco => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosBancarios_txtBanco"));

        IWebElement txtAgencia => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosBancarios_txtAgenciaCorrespondencia"));

        IWebElement txtAgenciaDigito => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosBancarios_txtDigitoAgenciaCorrespondencia"));

        IWebElement txtContaCliente => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosBancarios_txtContaCorrespondencia"));

        IWebElement txtContaDigito => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_ucDadosBancarios_txtDigitoContaCorrespondencia"));

        IWebElement btnprosseguir2 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucDadosCliente_btnProsseguir"));

        #endregion

        #region Mapeamento dos elementos Conta Bancária Terceiros

        IWebElement cbxTipoContaTerceiro => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucContinuacaoDigitacao_ucAccordion3_ddlTipoConta"));

        IWebElement txtBancoTerceiro => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucContinuacaoDigitacao_ucAccordion3_txtBanco"));

        IWebElement txtAgenciaTerceiro => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucContinuacaoDigitacao_ucAccordion3_txtAgencia"));

        IWebElement txtAgenciaDigitoTerceiro => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucContinuacaoDigitacao_ucAccordion3_txtDvAgencia"));

        IWebElement txtContaTerceiro => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucContinuacaoDigitacao_ucAccordion3_txtConta"));

        IWebElement txtContaDigitoTerceiro => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucContinuacaoDigitacao_ucAccordion3_txtDvConta"));

        #endregion

        #region Mapeamento dos elementos Dados de Averbação

        IWebElement txtCodAverbacao => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucContinuacaoDigitacao_ucAccordion1_txtCodAverbacao"));

        IWebElement txtCodAutenticacao => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucContinuacaoDigitacao_ucAccordion1_txtCodAutenticacao"));

        #endregion

        #region Mapeamento dos elementos de Geração Final da Proposta 

        IWebElement btnProsseguirObservacao => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucContinuacaoDigitacao_btnProsseguir"));

        IWebElement btnProsseguirSalvar => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_ucObservacaoProposta_ucBlocoObservacao_btnProsseguir"));

        IWebElement numeroProposta => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_lblMotivo"));

        IWebElement msgCartaoCorrespondente => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_divCartaoCorrespondente"));

        IWebElement msgCartaoAtendente => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_divCartaoAtendente"));

        IWebElement linkCartao => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_linkCartao"));

        IWebElement telaOlaCartao => DriverFactory.INSTANCE.FindElement(By.XPath("//*[@id='wrapper']/section/div/div[1]/div/div/h1/span"));

        IWebElement btnAnexarDocumentacao => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_btnAnexarDocumentacao"));

        IWebElement msgErroProposta => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_vdsErroProposta"));


        #endregion

        #endregion

        #region Ações

        #region Ações Dados Inicio da Simulação

        public static string convenioselecionado { set; get; }
        public static string produtoselecionado { set; get; }
        public static string cpfcliente { set; get; }
        public static string numProposta { set; get; }
        public static string nomePastaDocumento { set; get; }
        public static string qntParcelasContratoPortado { set; get; }
        public static string qntParcelasTotaisContratoPortado { set; get; }
        public static string valorParcela { set; get; }
        public static string tipoVendaSelecionado { set; get; }
        public static double taxapreenchida { set; get; }
        public static List<string> totalParcelasConvenio { set; get; }
        public static List<string> fasePropostaNaAnexacao { set; get; }
        public static double taxaConvenio { set; get; }

        public void AbrirPaginaCadastroPortabilidade()
        {
            NavigateTo(ConfigurationManager.AppSettings["CadastroPortabilidade"]);
        }
        public void PreencherConvenio(string convenio)
        {
            if (IsVisible(By.Id("inputConvenio")) == true)
            {
                List<string> listNome = DatabaseFactory.DBRetornarDadosQuery(Scripts.retornaCodConvenio.Replace("@nomeconvenio", convenio));

                if (listNome != null)
                {
                    for (int i = 0; listNome.Count > i; i++)
                    {
                        convenioselecionado = listNome[i].TrimStart('0');

                        Click(btnConvenio);
                        SendKey(txtCodConvenio, listNome[i].TrimStart('0') + OpenQA.Selenium.Keys.ArrowDown
                                + OpenQA.Selenium.Keys.Enter);

                        Click(txtCodConvenio);
                    }
                }
            }
        }
        public void PreencherNome(string nome)
        {
            Click(txtNome);
            SendKey(txtNome, nome);
        }
        public void PreencherCPF(string cpf)
        {
            if (cpf == "Gerar Automatico")
            {
                cpfcliente = GeradorDeDados.GerarCpf();
                Click(txtCPF);
                SendKey(txtCPF, cpfcliente);
            }
            else
            {
                Click(txtCPF);
                SendKey(txtCPF, cpf);
                cpfcliente = cpf;
            }
        }
        public void PreencherDataNascimento(string datanascimento)
        {
            if (GetElement(txtDataNascimento).Enabled == true)
            {
                Click(txtDataNascimento);
                SendKey(txtDataNascimento, datanascimento);
            }
        }
        public void PreencherBancoPortabilidade(string codbancoportabilidade)
        {
            SendKey(txtBancoPortabilidade, codbancoportabilidade);
        }
        public void PreencherNumContrato(string numcontrato)
        {
            if (numcontrato == "Randon")
            {
                int de = 11111;
                int ate = 999999999;
                int contrato = Random(de, ate);

                Click(txtNumeroContrato);
                SendKeyInt(txtNumeroContrato, contrato);
            }
        }
        public void PreencherQntParcelas(string qntparcelas)
        {
            SendKey(txtQuantidadeParcelas, qntparcelas);
            qntParcelasContratoPortado = qntparcelas;
        }
        public void PreencherVlrParcela(string vlrparcela)
        {
            SendKey(txtValorParcelas, vlrparcela);
            valorParcela = vlrparcela;
        }
        public void ClicarRadionButtonTaxa()
        {
            Click(rbTaxa);
        }
        public void PreencherTaxaDoContratoPortado(string valor)
        {
            if (valor == "Taxa Maior do Convenio")
            {
                taxaConvenio = Funções.RecuperarMaiorTaxaProduto(0 + convenioselecionado);

                Click(txtTaxaContratoPortado);
                SendKeyDouble(txtTaxaContratoPortado, taxaConvenio);
                string texto = taxaConvenio.ToString();
                if (texto.Length == 4)
                {
                    Click(txtTaxaContratoPortado);
                    SendKey(txtTaxaContratoPortado, "00");
                }
                else if (texto.Length == 3)
                {
                    Click(txtTaxaContratoPortado);
                    SendKey(txtTaxaContratoPortado, "000");
                }
                else if (texto.Length == 2)
                {
                    Click(txtTaxaContratoPortado);
                    SendKey(txtTaxaContratoPortado, "0000");
                }
                taxapreenchida = taxaConvenio;
            }
            else
            {
                Click(txtTaxaContratoPortado);
                SendKey(txtTaxaContratoPortado, valor);

                string texto = valor.ToString();
                if (texto.Length == 4)
                {
                    Click(txtTaxaContratoPortado);
                    SendKey(txtTaxaContratoPortado, "00");
                }
                else if (texto.Length == 3)
                {
                    Click(txtTaxaContratoPortado);
                    SendKey(txtTaxaContratoPortado, "000");
                }
                else if (texto.Length == 2)
                {
                    Click(txtTaxaContratoPortado);
                    SendKey(txtTaxaContratoPortado, "0000");
                }
            }
        }
        public void PreencherTotalParcProduto(string totalparcela)
        {
            if (totalparcela == "Total de Parcelas Convenio")
            {
                totalParcelasConvenio = new List<string>(DatabaseFactory.DBRetornarDadosQuery(Scripts.retornatotalparc.Replace("@convenio", convenioselecionado)));

                if (totalParcelasConvenio != null)
                {
                    for (int i = 0; i < totalParcelasConvenio.Count; ++i)
                    {
                        Clear(txtQtdTotalParcelas);
                        SendKey(txtQtdTotalParcelas, totalParcelasConvenio[0]);
                        qntParcelasTotaisContratoPortado = totalParcelasConvenio[0];
                    }
                }
                else
                {
                    Console.WriteLine("Não foi encontrado produto na query retornada anteriormente. ");
                }
            }
            else
            {
                Clear(txtQtdTotalParcelas);
                SendKey(txtQtdTotalParcelas, totalparcela);
                qntParcelasTotaisContratoPortado = totalparcela;
            }
            
        }
        public void ClicarTipoVenda(string tipovenda)
        {
            if (tipovenda == "Padrao")
            {
                tipoVendaSelecionado = "P";
                Click(rbTipoVendaPadrao);
                Click(btnSim);
            }                
            else if (tipovenda == "Digital")
            {
                tipoVendaSelecionado = "D";
                Click(rbTipoVendaDigital);
            }
        }
        public void ClicarProsseguirCarregaProdutos()
        {
            Click(btnProsseguirPrd);
        }

        public string RetornarMsgValidacao()
        {
           string msg = RetornaTextLabel(By.Id("vdsPagina"));
           return msg;
        }
        #endregion

        #region Ações Selecionar Produto

        public List<string> ListaDeProdutos()
        {
            List<string> listaOpcoesCbxProduto = new List<string>(GetAllOptionsCombo(By.Id("ddlProduto")));
            return listaOpcoesCbxProduto;
        }
        public void SelecionarProduto()
        {
            Click(cbxProduto);

            List<string> qntprodutosLista = new List<string>(ListaDeProdutos());
            var produtoRandomico = Random(1, qntprodutosLista.Count);
            SelectIndex(cbxProduto, produtoRandomico);
            GeneralHelpers.ScrollTo(btnSimular);
            produtoselecionado = GetSelectedText(cbxProduto);
        }
        public void ClicarSimular()
        {
            Click(btnSimular);
        }

        #endregion

        #region Ações Simulação
        public string RetornaParcelasAbertas()
        {
            return GetElementTextValue(saldodevedor);
        }
        public string RetornaQntParcelas()
        {
            return GetElementTextValue(parcelasabertos);
        }
        public string RetornaValorParcelas()
        {
            return GetElementTextValue(vlrparcelas);
        }
        public string RetornaValorContrato()
        {
            return GetElementTextValue(vlrcontrato);
        }
        public string RetornaValorLiberado()
        {
            return GetElementTextValue(vlrliberado);
        }
        public string RetornaCustoOriginacao()
        {
            return GetElementTextValue(vlrcustooriginacao);
        }
        public string RetornaMotivoSimulacao()
        {
            return GetElementTextValue(motivosimulacao);
        }
        public string RetornaTaxaPonderada()
        {
            return GetElementTextValue(vlrtaxaponderada);
        }
        public void ClicarGravarSimulacao()
        {
            var resultadoSimulacao = RetornaTextLabel(By.Id("lblMotivo"));
            Console.WriteLine(resultadoSimulacao);
            if (resultadoSimulacao == "Portabilidade Aprovada.")
            {
                if (IsEnabled(By.Id("btnSalvar")) == true)
                {
                    if (IsVisible(By.Id("btnSalvar")) == true)
                    {
                        Click(btnGravarSimulacao);
                    }
                }
            }
            else
            {
                Console.WriteLine("Simulação não aprovada, verificar o erro.");
            }
        }
        public void ClicarEditarSimulacao()
        {
            btnEditarSimulacao.Click();
        }

        #endregion

        #region Ações Login Portabilidade

        public void PreencherUsuarioPortabilidade(string usuarioport)
        {
            SendKey(txtUsuario, usuarioport);
        }
        public void PreencherSenhaPortabilidade(string senhaport)
        {
            SendKey(txtSenha, senhaport);
        }
        public void ClicarEntrar()
        {
            Click(btnEntrar);
        }

        #endregion

        #region Ações Dados Principais da Proposta

        public void RetornaProdutoPort()
        {
            var texto = GetElementTextValue(txtProdPort);
        }
        public void SelecionarLojaAleatoria()
        {
            Thread.Sleep(1500);
            if (IsEnabled(By.Id("MainContent_ucDadosIniciais_ucAccordion1_ddlLoja")) == true)
            {
                if (IsVisible(By.Id("MainContent_ucDadosIniciais_ucAccordion1_ddlLoja")) == true)
                {
                    if (GetSelectedText(cbxLoja) == "Selecione")
                    {
                        Click(cbxLoja);
                        SelectIndex(cbxLoja, 1);
                    }
                }
            }
        }
        public void SelecionarOrgaoAleatorio()
        {
            if (IsVisible(By.Id("MainContent_ucDadosIniciais_ucAccordion1_ddlOrgao")))
            {
                if (GetSelectedText(cbxOrgao) == "Selecione")
                {
                    SelectIndex(cbxOrgao, 1);
                }
            }
        }
        public void PreencherCodigoBeneficio()
        {
            if (CampoOculto(By.Id("MainContent_ucDadosIniciais_ucAccordion1_pCodigoBeneficio")) == false)
            {
                if (string.IsNullOrEmpty(GetElementTextValue(txtCodBeneficio)))
                {
                    Click(txtCodBeneficio);
                    if (convenioselecionado == "13579" || convenioselecionado == "11398")
                    {
                        SendKey(txtCodBeneficio, "21");
                    }
                }
            }
        }
        public void PreencherVersaoFormulario(string versao)
        {
            SendKey(txtVersaoFormulario, versao);
        }
        public void PreencherRenda(string renda)
        {
            SendKey(txtRenda, renda);
        }
        public void ClicarProsseguir()
        {
            Click(btnProsseguir);
            Thread.Sleep(3000);
        }
        public void ClicarVoltar()
        {
            Click(btnVoltar);
        }

        #endregion

        #region Ações Cadastro do Cliente

        public void PreencherNomeCliente(string nome)
        {
            Wait(txtnomecliente);
            if (IsEnabled(By.Id("MainContent_ucDadosCliente_ucDadosCliente_txtNome")) == true)
            {
                if (string.IsNullOrEmpty(GetElementTextValue(txtnomecliente)))
                {
                    Click(txtnomecliente);
                    SendKey(txtnomecliente, nome);
                }
            }
        }
        public void SelecionarNacionalidade(string nacionalidade)
        {
            if (GetSelectedText(cbxNacionalidade) == "Selecione")
            {
                SelectValue(cbxNacionalidade, nacionalidade);
            }
        }
        public void PreencherNaturalidade(string naturalidade)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtNaturalidade)))
            {
                Click(txtNaturalidade);
                SendKey(txtNaturalidade, naturalidade);
            }
        }
        public void SelecionarSexo(string sexo)
        {
            if (GetSelectedText(cbxSexo) == "Selecione")
            {
                SelectValue(cbxSexo, sexo);
            }
        }
        public void SelecionarEstadoCivil(string estadocivil)
        {
            if (GetSelectedText(cbxEstadoCivil) == "Selecione")
            {
                SelectValue(cbxEstadoCivil, estadocivil);
            }
        }
        public void PreencherRG(string rg)
        {
            Thread.Sleep(2000);
            if (string.IsNullOrEmpty(GetElementTextValue(txtRG)))
            {
                Click(txtRG);
                SendKey(txtRG, rg);
            }
        }
        public void PreencherUfRg(string ufrg)
        {
            if (GetSelectedText(cbxUFRG) == "Selecione")
            {
                SelectValue(cbxUFRG, ufrg);
            }
        }
        public void PreencherOrgaoEmissor(string orgaoemissor)
        {
            Thread.Sleep(1000);
            if (string.IsNullOrEmpty(GetElementTextValue(txtOrgaoEmissor)))
            {
                Click(txtOrgaoEmissor);
                SendKey(txtOrgaoEmissor, orgaoemissor);
            }
        }
        public void PreencherDataEmissao(string dataemissao)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtDataEmissao)))
            {
                Click(txtDataEmissao);
                SendKey(txtDataEmissao, dataemissao);
            }
        }
        public void PreencherCep(string cep)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtCEP)))
            {
                Click(txtCEP);
                SendKey(txtCEP, cep);
            }
        }
        public void PreencherUF(string uf)
        {
            if (GetSelectedText(cbxUF) == "Selecione")
            {
                SelectValue(cbxUF, uf);
            }
        }
        public void PreencherEndereco(string endereco)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtEndereco)))
            {
                Click(txtEndereco);
                SendKey(txtEndereco, endereco);
            }
        }
        public void PreencherNumero(string numero)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtNumero)))
            {
                Click(txtNumero);
                SendKey(txtNumero, numero);
            }
        }
        public void PreencherBairro(string bairro)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtBairro)))
            {
                Click(txtBairro);
                SendKey(txtBairro, bairro);
            }
        }
        public void PreencherCidade(string cidade)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtCidade)))
            {
                Click(txtCidade);
                SendKey(txtCidade, cidade);
            }
        }
        public void PreencherDDDTel(string dddtel)
        {
            var a = GetElementTextValue(txtDDDTel);
            if (a == null)
            {
                Click(txtDDDTel);
                SendKey(txtDDDTel, dddtel);
            }
        }
        public void PreencherNumeroTel(string numtel)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtNumeroTelefone)))
            {
                Click(txtNumeroTelefone);
                if (numtel == "Randon")
                {
                    int de = 33221111;
                    int ate = 33900000;
                    int numero = Random(de, ate);
                    SendKey(txtNumeroTelefone, numero.ToString());
                }
                else
                {
                    SendKey(txtNumeroTelefone, numtel);
                }
            }
        }
        public void PreencherDDDCel(string dddcel)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtDDDCel)))
            {
                Click(txtDDDCel);
                SendKey(txtDDDCel, dddcel);
            }
        }
        public void PreencherNumeroCel(string numcel)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtNumeroCelular)))
            {
                Click(txtNumeroCelular);
                if (numcel == "Randon")
                {
                    int de = 75222222;
                    int ate = 89999999;
                    int numero = Random(de, ate);
                    SendKey(txtNumeroCelular, 9 + numero.ToString());
                }
                else
                {
                    SendKey(txtNumeroCelular, numcel);
                }
            }
        }
        public void PreencherMatricula(string matricula)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtMatricula)))
            {
                Click(txtMatricula);
                if (matricula == "Randon")
                {
                    if (convenioselecionado == "13579")
                    {
                        SendKey(txtMatricula, "10101010");
                    }
                    else if (convenioselecionado == "11398" || convenioselecionado == "12050")
                    {
                        SendKey(txtMatricula, "1010101010");
                    }
                    else if (convenioselecionado == "14583" || convenioselecionado == "11533" || convenioselecionado == "10421" || convenioselecionado == "14699")
                    {
                        if (cpfcliente == "52857463103")
                            SendKey(txtMatricula, "0000105582");
                        else if (cpfcliente == "72520825120")
                            SendKey(txtMatricula, "0000105583");
                        else if (cpfcliente == "68367533275")
                            SendKey(txtMatricula, "0000105584");
                        else if (cpfcliente == "04603097220")
                            SendKey(txtMatricula, "0000105585");
                        else if (cpfcliente == "22665772124")
                            SendKey(txtMatricula, "0000105586");
                        else if (cpfcliente == "81048751910")
                            SendKey(txtMatricula, "0000105587");
                        else if (cpfcliente == "66344965525")
                            SendKey(txtMatricula, "0000105588");
                        else if (cpfcliente == "89781300620")
                            SendKey(txtMatricula, "0000105589");
                        else
                            SendKey(txtMatricula, "0000105589");
                    }
                }
                else
                {
                    SendKey(txtMatricula, matricula);
                }
            }
        }
        public void PreencherNomePai(string nomepai)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtNomePai)))
            {
                Click(txtNomePai);
                SendKey(txtNomePai, nomepai);
            }
        }
        public void PreencherNomeMae(string nomemae)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtNomeMae)))
            {
                Click(txtNomeMae);
                SendKey(txtNomeMae, nomemae);
            }
        }
        public void PreencherEmail(string email)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtEmail)) & tipoVendaSelecionado == "D")
            {
                if (email == "Email Válido")
                {
                    ValidaEmail util = new ValidaEmail();
                    List<string> listaEmailValido = new List<string>();
                    listaEmailValido.Add("david.jones@proseware.com");
                    listaEmailValido.Add("d.j@server1.proseware.com");
                    listaEmailValido.Add("jones@ms1.proseware.com");
                    listaEmailValido.Add("js#internal@proseware.com");
                    listaEmailValido.Add("j_9@[129.126.118.1]");
                    listaEmailValido.Add("j.s@server1.proseware.com");

                    foreach (var emailAddress in listaEmailValido)
                    {
                        if (util.IsValidEmail(emailAddress))
                        {
                            int valor = Random(0, listaEmailValido.Count);

                            Click(txtEmail);
                            SendKey(txtEmail, listaEmailValido[valor]);
                        }
                        else
                        {
                            email = "E-mail inválido";
                            PreencherEmail(email);
                        }
                    }
                }
                else if(email == "Email Invalido")
                {
                    ValidaEmail util = new ValidaEmail();
                    List<string> listaEmailInvalido = new List<string>();
                    listaEmailInvalido.Add("j.@server1.proseware.com");
                    listaEmailInvalido.Add("j..s@proseware.com");
                    listaEmailInvalido.Add("js*@proseware.com");
                    listaEmailInvalido.Add("js@proseware..com");

                    foreach (var emailAddress in listaEmailInvalido)
                    {
                        if (!util.IsValidEmail(emailAddress))
                        {
                            int valor = Random(0, listaEmailInvalido.Count);

                            Click(txtEmail);
                            SendKey(txtEmail, listaEmailInvalido[valor]);
                        }
                        else
                        {
                            email = "Email Válido";
                            PreencherEmail(email);
                        }
                    }
                }
                else
                {
                    Click(txtEmail);
                    SendKey(txtEmail, email);
                }
            }
        }

        #endregion

        #region Ações Dados Bancários do Cliente

        public void SelecionarTipoContaCliente(string tipocontacliente)
        {
            if (GetSelectedText(cbxTipoConta) == "Selecione")
            {
                SelectValue(cbxTipoConta, tipocontacliente);
            }
        }
        public void PreencherBancoCliente(string bancocliente)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtBanco)))
            {
                SendKey(txtBanco, bancocliente);
            }
        }
        public void PreencherAgenciaCliente(string agenciacliente)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtAgencia)))
            {
                Click(txtAgencia);
                SendKey(txtAgencia, agenciacliente);
            }
        }
        public void PreencherDigitoAgenciaCliente(string digitoagenciacliente)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtAgenciaDigito)))
            {
                Click(txtAgenciaDigito);
                SendKey(txtAgenciaDigito, digitoagenciacliente);
            }
        }
        public void PreencherContaCliente(string contacliente)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtContaCliente)))
            {
                Click(txtContaCliente);
                SendKey(txtContaCliente, contacliente);
            }
        }
        public void PreencherDigitoContaCliente(string digitocontacliente)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtContaDigito)))
            {
                Click(txtContaDigito);
                SendKey(txtContaDigito, digitocontacliente);
            }
        }
        public void ClicarBtnProsseguirDados()
        {
            Click(btnprosseguir2);
            Thread.Sleep(3000);
        }
        public bool ValidaMsgEmailInvalido()
        {
            if (ValidaElementTextLabel(By.Id("MainContent_ucDadosCliente_ucDadosCliente_vdsDadosCliente"), "O e-mail informado não é um e-mail válido.") == true)
            {
                return true;
            }
            else return false;
        }

        #endregion

        #region Ações Conta Bancária Terceiros

        public void PreencherTipoContaTerceiro(string tipocontaterceiro)
        {
            Thread.Sleep(1000);
            if (GetSelectedText(cbxTipoContaTerceiro) == "Selecione")
            {
                SelectValue(cbxTipoContaTerceiro, tipocontaterceiro);
            }
        }
        public void PreencherBancoTerceiro(string bancoterceiro)
        {
            if (string.IsNullOrEmpty(GetElementTextValue(txtBancoTerceiro)))
            {
                Click(txtBancoTerceiro);
                SendKey(txtBancoTerceiro, bancoterceiro);
            }
        }
        public void PreencherAgenciaTerceiro(string agenciaterceiro)
        {
            if (GetElementText(txtAgenciaTerceiro) == null)
            {
                Click(txtAgenciaTerceiro);
                SendKey(txtAgenciaTerceiro, agenciaterceiro);
            }
        }
        public void PreencherDigitoAgenciaTerceiro(string digitoagenciaterceiro)
        {
            if (GetElementTextValue(txtAgenciaDigitoTerceiro) == null)
            {
                Click(txtAgenciaDigitoTerceiro);
                SendKey(txtAgenciaDigitoTerceiro, digitoagenciaterceiro);
            }
        }
        public void PreencherContaTerceiro(string contaterceiro)
        {
            if (GetElementTextValue(txtContaTerceiro) == null)
            {
                Click(txtContaTerceiro);
                SendKey(txtContaTerceiro, contaterceiro);
            }
        }
        public void PreencherDigitoContaTerceiro(string digitocontaterceiro)
        {
            if (GetElementTextValue(txtContaDigitoTerceiro) == null)
            {
                Click(txtContaDigitoTerceiro);
                SendKey(txtContaDigitoTerceiro, digitocontaterceiro);
            }
        }

        #endregion

        #region Ações Dados de Averbação

        public void PreencherCodAverbacao(string codaverbacao)
        {
            if (IsVisible(By.Id("MainContent_ucContinuacaoDigitacao_ucAccordion1_txtCodAverbacao")) == true)
            {
                if (string.IsNullOrEmpty(GetElementTextValue(txtCodAverbacao)))
                {
                    Click(txtCodAverbacao);
                    SendKey(txtCodAverbacao, codaverbacao);
                }
            }
        }
        public void PreencherCodAutenticacao(string codautenticacao)
        {
            if (IsVisible(By.Id("MainContent_ucContinuacaoDigitacao_ucAccordion1_txtCodAutenticacao")) == true)
            {
                if (string.IsNullOrEmpty(GetElementTextValue(txtCodAutenticacao)))
                {
                    Click(txtCodAutenticacao);
                    SendKey(txtCodAutenticacao, codautenticacao);
                }
            }
        }

        #endregion

        #region Ações Geração Final da Proposta

        public void ClicarProsseguirObservacao()
        {
            Click(btnProsseguirObservacao);
        }
        public void ClicarProsseguirFinal()
        {
            Thread.Sleep(5000);
            IsEnabled(By.Id("MainContent_ucObservacaoProposta_ucBlocoObservacao_btnProsseguir"));
            Click(btnProsseguirSalvar);
        }
        public void GeraçãoNumeroProposta()
        {
            Thread.Sleep(30000);
            string texto = "Proposta incluída com sucesso.";

            if (IsEnabled(By.Id("MainContent_lblMotivo")) == true)
            {
                if (IsVisible(By.Id("MainContent_lblMotivo")) == true)
                {
                    if (IsInvisibilit(By.Id("MainContent_lblMotivo")) == false)
                    {
                        var valor = RetornaTextLabel(By.Id("MainContent_lblMotivo"));
                        if (ValidaElementTextLabel(By.Id("MainContent_lblMotivo"), texto) == true)
                        {
                            numProposta = Funções.RetornaNumProposta(valor);
                            Console.WriteLine("Proposta incluída com sucesso: " + numProposta);
                        }
                    }
                }
            }
        }

        public void ClicarAnexarNaPortabilidade()
        {
            if (IsEnabled(By.Id("MainContent_btnAnexarDocumentacao")) == true)
                Click(btnAnexarDocumentacao);
        }
        public string ValidaMensagemDirecionamentoCartao(string cpf)
        {
            string msgAtendente = "ATENÇÃO: Este cliente também pode ter um contrato de cartão.Direcione-o para o setor responsável.";
            string msgCorrespondente = "ATENÇÃO: Este cliente também pode ter um contrato de cartão.Clique aqui para contratar.";
            string cpfMsgAtendente = "196.325.534-89"; //clientePodeFazerCartao = true  correspondentePossuiProdutoCartao = true  usuarioPossuiPermissaoDigitarCartao = false
            string cpfMsgCorrespondente = "275.954.652-73"; //clientePodeFazerCartao = true  correspondentePossuiProdutoCartao = true  usuarioPossuiPermissaoDigitarCartao = true

            List<string> cpfsNaoPodemExibirMsg = new List<string>();

            cpfsNaoPodemExibirMsg.Add("277.745.386-15");//clientePodeFazerCartao = true 
                                                        //correspondentePossuiProdutoCartao = false 
                                                        //usuarioPossuiPermissaoDigitarCartao = false
            cpfsNaoPodemExibirMsg.Add("487.669.122-37");//clientePodeFazerCartao = false
                                                        //correspondentePossuiProdutoCartao = true
                                                        //usuarioPossuiPermissaoDigitarCartao = false
            cpfsNaoPodemExibirMsg.Add("568.193.919-78");//clientePodeFazerCartao = false
                                                        //correspondentePossuiProdutoCartao = true
                                                        //usuarioPossuiPermissaoDigitarCartao = false
            cpfsNaoPodemExibirMsg.Add("662.265.874-90");//clientePodeFazerCartao = false
                                                        //correspondentePossuiProdutoCartao = false
                                                        //usuarioPossuiPermissaoDigitarCartao = true
            cpfsNaoPodemExibirMsg.Add("986.898.164-61");//clientePodeFazerCartao = false
                                                        //correspondentePossuiProdutoCartao = false
                                                        //usuarioPossuiPermissaoDigitarCartao = false

            if (cpf == null || cpf == "")
            {
                Console.WriteLine("CPF sem disponibilidade de cartão e/ou usuário não é correspondente/atendente.");
                return null;
            }
            else if (!cpfsNaoPodemExibirMsg.Contains(cpf))
            {
                Console.WriteLine("CPF sem disponibilidade de cartão e/ou usuário não é correspondente/atendente.");
                return null;
            }
            else if (cpf == cpfMsgAtendente)
            {
                if (RetornaTextLabel(By.Id("MainContent_divCartaoAtendente")) == msgAtendente)
                {
                    Console.WriteLine("Mensagem correta do atendente: " + RetornaTextLabel(By.Id("MainContent_divCartaoAtendente")));
                }
                return msgAtendente;
            }
            else if (cpf == cpfMsgCorrespondente)
            {
                if (RetornaTextLabel(By.Id("MainContent_divCartaoCorrespondente")) == msgCorrespondente)
                {
                    Click(linkCartao);
                    var telacartao = "BEM-VINDO AO";
                    Switch();
                    if (RetornaTextLabel(By.XPath("//*[@id='wrapper']/section/div/div[1]/div/div/h1/span")) == telacartao)
                    {
                        Console.WriteLine("Direcionamento correto para o Olá");
                    }
                }
                return msgCorrespondente;
            }
            else
            {
                Console.WriteLine("CPF não está configurado no dummy de mensagem de cartão, então msg não é exibida.");
                return null;
            }
        }
        public void DeletaPropostaCriadaPorCPF(string cpf)
        {
            DatabaseFactory.DBExecutarQuery(Scripts.delpropostaCMOVP.Replace("@cpf", cpf));

            DatabaseFactory.DBExecutarQuery(Scripts.delpropostaEPROP.Replace("@cpf", cpf));

            DatabaseFactory.DBExecutarQuery(Scripts.delpropostaCOBSE.Replace("@cpf", cpf));

            DatabaseFactory.DBExecutarQuery(Scripts.delpropostaSimulador.Replace("@cpf", cpf.Replace(".", "").Replace("-", "")));

            DatabaseFactory.DBExecutarQuery(Scripts.delpropostaEmprestimo.Replace("@cpf", cpf));

            DatabaseFactory.DBExecutarQuery(Scripts.delpropostaFT01.Replace("@cpf", cpf));

            DatabaseFactory.DBExecutarQuery(Scripts.delpropostaCRENP.Replace("@cpf", cpf));

            DatabaseFactory.DBExecutarQuery(Scripts.delpropostaCPROP.Replace("@cpf", cpf));
        }
        public void FechaTela()
        {
            Close();
        }

        #endregion

        #endregion
    }
}