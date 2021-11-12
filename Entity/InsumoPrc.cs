using System;
using System.Collections.Generic;
using System.Text;

namespace OrcaFascio.Entity
{
    public class InsumoPrc : EntityBase
    {

        public int CodColigada { get; set; }

        public string CodUnd { get; set; }

        public string DescIsmPrc { get; set; }

        public int? IdIsm { get; set; }

        public int IdIsmPrc { get; set; }

        public int IdPrj { get; set; }
        
        public double Valor { get; set; }

        public InsumoPrc()
        {
        }

        public override string ToString()
        {
            return IdIsmPrc + " - "+Valor;
        }
    }
}
