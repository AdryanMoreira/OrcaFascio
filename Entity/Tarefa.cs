using System;
using System.Collections.Generic;
using System.Text;

namespace OrcaFascio.Entity
{
    public class Tarefa : EntityBase
    {

        public string CodCmp { get; set; }

        public int CodColigada { get; set; }

        public string CodUnd { get; set; }

        public string CodTrfPai { get; set; }

        public string CodTrf { get; set; }

        public int? IdCmp { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public int IdPrj { get; set; }

        public int IdTrf { get; set; }

        public int? IdPai { get; set; }

        public double? CustoUnit { get; set; }

        public int Servico { get; set; }

        public double? Quantidade { get; set; }
    }
}
