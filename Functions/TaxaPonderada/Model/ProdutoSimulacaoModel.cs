
namespace TaxaConvenio.Model
{
    public class ProdutoSimulacaoModel
    {
        #region Propriedades

        public string CodigoProduto { get; set; }
        public string NomeProduto { get; set; }
        public string Descricao { get; set; }
        public string CodigoConvenio { get; set; }
        public string CodigoTipoProduto { get; set; }
        public string TipoProduto
        {
            get
            {
                switch (CodigoTipoProduto.Trim().ToUpper())
                {
                    case ";03":
                        return "REFIN";
                    case "13":
                        return "PORTABILIDADE";
                    case "01":
                        return "CONTRATO NOVO";
                    case "08":
                        return "CARTÃO";
                    case "09":
                        return "RECOMPRA";
                    default:
                        return string.Empty;
                }
            }
        }
        public string TipoPrecificacao { get; set; }
        public int Faixa1_Min { get; set; }
        public int Faixa1_Max { get; set; }
        public int Faixa2_Min { get; set; }
        public int Faixa2_Max { get; set; }
        public int Faixa3_Min { get; set; }
        public int Faixa3_Max { get; set; }
        public int Faixa4_Min { get; set; }
        public int Faixa4_Max { get; set; }
        public int Faixa5_Min { get; set; }
        public int Faixa5_Max { get; set; }
        public int Faixa6_Min { get; set; }
        public int Faixa6_Max { get; set; }
        public double Taxa1 { get; set; }
        public double Taxa2 { get; set; }
        public double Taxa3 { get; set; }
        public double Taxa4 { get; set; }
        public double Taxa5 { get; set; }
        public double Taxa6 { get; set; }

        #endregion
    }
}
