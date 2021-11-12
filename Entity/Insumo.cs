using System;
using System.Collections.Generic;
using System.Text;

namespace OrcaFascio.Entity
{
    public class Insumo : EntityBase
    {

        public int CodColigada { get; set; }

        public int IdIsm { get; set; }

        public int? IdIsmPrc { get; set; }

        public int IdPrj { get; set; }
        
        public string Banco { get; set; }

        public string CodIsm { get; set; }
        
        public string DescIsm { get; set; }

        public string GrupoDner { get; set; }

        public string CodUnd { get; set; }
        
        public int IdGis { get; set; }

        public double Valor { get; set; }

        public Insumo()
        {
        }

        public override string ToString()
        {
            return CodIsm + " - "+DescIsm;
        }
    }
}
