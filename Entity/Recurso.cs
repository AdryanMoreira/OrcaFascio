using System;
using System.Collections.Generic;
using System.Text;

namespace OrcaFascio.Entity
{
    public class Recurso : EntityBase
    {
        public int CodColigada { get; set; }

        public int IdCmp { get; set; }

        public int? IdCmpFilha { get; set; }

        public int? IdIsm { get; set; }

        public int IdPrj { get; set; }

        public int IdRec { get; set; }

        public string CodCmpPrincipal { get; set; }

        public string CodCmpFilha { get; set; }

        public string CodIsm { get; set; }

        public double Quantidade { get; set; }

        public double ValorUnit { get; set; }
        
        public double ValorTotal { get; set; }
    }
}
