using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Application.Model
{
    public class ProdutoModel
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string SituacaoProduto { get; set; }
        public Nullable<DateTime> DataFabricacao { get; set; }
        public Nullable<DateTime> DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CnpjFornecedor { get; set; }
    }
}
