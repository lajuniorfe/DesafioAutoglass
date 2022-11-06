using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Entities
{
    public class Produtos : BaseEntity
    {
      
        public string Descricao { get; set; }

        public string Situacao { get; set; }

        public  DateTime Data_Fabricacao { get; set; }

        public DateTime Data_Validade { get; set; }

        public int Codigo_Fornecedor { get; set; }

        public string Descricao_Fornecedor { get; set; }

        public string Cnpj { get; set; }
    }
}
