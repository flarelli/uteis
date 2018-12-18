using System;
using TaxaConvenio.Dados;
using TaxaConvenio.Model;

namespace TaxaConvenio.Negocio
{
    public class ProdutoSimulacaoNegocio
    {
        #region Métodos Públicos

        /// <summary>
        /// Recupera o produto por código produto
        /// </summary>
        /// <param name="codigoProduto"></param>
        /// <returns></returns>
        public ProdutoSimulacaoModel RecuperarProduto(string codigoProduto)
        {
            return new ProdutoSimulacaoDados().RecuperarParametrosProduto(codigoProduto);
        }

        /// <summary>
        /// Recupera os produtos de PORT do convênio
        /// </summary>
        /// <param name="codigoConvenio"></param>
        /// <returns></returns>
        public ProdutoSimulacaoModel[] RecuperarProdutosPort(string codigoConvenio)
        {
            return new ProdutoSimulacaoDados().RecuperarParametrosProdutosPort(codigoConvenio);
        }

        /// <summary>
        /// Encontrar a maior taxa do produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public double EncontrarMaiorTaxa(ProdutoSimulacaoModel produto)
        {
            double taxa = 0;

            if (produto.Taxa1 > taxa)
                taxa = produto.Taxa1;
            if (produto.Taxa2 > taxa)
                taxa = produto.Taxa2;
            if (produto.Taxa3 > taxa)
                taxa = produto.Taxa3;
            if (produto.Taxa4 > taxa)
                taxa = produto.Taxa4;
            if (produto.Taxa5 > taxa)
                taxa = produto.Taxa5;
            if (produto.Taxa6 > taxa)
                taxa = produto.Taxa6;

            return taxa;
        }

        /// <summary>
        /// Encontrar o máximo de parcelas do produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public int EncontrarMaiorFaixa(ProdutoSimulacaoModel produto)
        {
            int prazoMaximo = 0;

            if (produto.Faixa1_Max > prazoMaximo)
                prazoMaximo = produto.Faixa1_Max;
            if (produto.Faixa2_Max > prazoMaximo)
                prazoMaximo = produto.Faixa2_Max;
            if (produto.Faixa3_Max > prazoMaximo)
                prazoMaximo = produto.Faixa3_Max;
            if (produto.Faixa4_Max > prazoMaximo)
                prazoMaximo = produto.Faixa4_Max;
            if (produto.Faixa5_Max > prazoMaximo)
                prazoMaximo = produto.Faixa5_Max;
            if (produto.Faixa6_Max > prazoMaximo)
                prazoMaximo = produto.Faixa6_Max;

            return prazoMaximo;
        }

        /// <summary>
        /// Retorna uma estrutura com o produto de maior taxa.
        /// Tuple<Produto(string), Taxa(decimal), FaixaMaximaConvenio(int)>
        /// </summary>
        /// <param name="produtos"></param>
        /// <returns></returns>
        public Tuple<string, double, int, int> EncontrarMaiorTaxa(ProdutoSimulacaoModel[] produtos)
        {
            string produto = string.Empty;
            double taxa = 0;
            int prazoMaximo = 0;
            int fator = 0;

            foreach (var p in produtos)
            {
                double taxaAnterior = taxa;

                taxa = EncontrarMaiorTaxa(p);

                if (taxa == taxaAnterior)
                    continue;

                produto = p.CodigoProduto;
                fator = Convert.ToInt16(p.TipoPrecificacao);

                prazoMaximo = EncontrarMaiorFaixa(p);
            }

            return new Tuple<string, double, int, int>(produto, taxa, prazoMaximo, fator);
        }

        /// <summary>
        /// Retorna uma estrutura com o produto de maior taxa.
        /// Tuple<Produto(string), Taxa(decimal), FaixaMaximaConvenio(int)>
        /// </summary>
        /// <param name="produtos"></param>
        /// <returns></returns>
        public Tuple<string, double, int, int> EncontrarDadosProduto(ProdutoSimulacaoModel produto)
        {
            string codProduto = string.Empty;
            double taxa = 0;
            int prazoMaximo = 0;
            int fator = 0;

            taxa = EncontrarMaiorTaxa(produto);
            codProduto = produto.CodigoProduto;
            fator = Convert.ToInt16(produto.TipoPrecificacao);
            prazoMaximo = EncontrarMaiorFaixa(produto);

            return new Tuple<string, double, int, int>(codProduto, taxa, prazoMaximo, fator);
        }

        /// <summary>
        /// Encontra uma taxa parametrizada a partir do total de parcelas
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="totalParcelas"></param>
        /// <returns></returns>
        public double EncontrarTaxaPorFaixa(ProdutoSimulacaoModel produto, int totalParcelas)
        {
            if (totalParcelas >= produto.Faixa1_Min && totalParcelas <= produto.Faixa1_Max)
                return produto.Taxa1;
            else if (totalParcelas >= produto.Faixa2_Min && totalParcelas <= produto.Faixa2_Max)
                return produto.Taxa2;
            else if (totalParcelas >= produto.Faixa3_Min && totalParcelas <= produto.Faixa3_Max)
                return produto.Taxa3;
            else if (totalParcelas >= produto.Faixa4_Min && totalParcelas <= produto.Faixa4_Max)
                return produto.Taxa4;
            else if (totalParcelas >= produto.Faixa5_Min && totalParcelas <= produto.Faixa5_Max)
                return produto.Taxa5;
            else if (totalParcelas >= produto.Faixa6_Min && totalParcelas <= produto.Faixa6_Max)
                return produto.Taxa6;
            else
                return 0;
        }

        #endregion
    }
}
