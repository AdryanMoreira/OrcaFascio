using System;
using System.Collections.Generic;
using System.Text;

namespace OrcaFascio.Entity
{
    public class Composicao : EntityBase
    {
        public string CodCmp { get; set; }

        public int CodColigada { get; set; }

        public string CodUnd { get; set; }

        public string DescCmp { get; set; }

        public string Banco { get; set; }

        public int IdCmp { get; set; }

        public int IdPrj { get; set; }

        public double ValorBdi { get; set; }
        
        public double ValorTotal { get; set; }

        public List<Recurso> recursos { get; set; } = new List<Recurso>();
    }
}
