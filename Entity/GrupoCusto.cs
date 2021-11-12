using System;
using System.Collections.Generic;
using System.Text;

namespace OrcaFascio.Entity
{
    public class GrupoCusto : EntityBase
    {
        public int CodColigada { get; set; }

        public int IdPrj { get; set; }

        public int IdGis { get; set; }

        public string CodGis { get; set; }

        public string DescGis { get; set; }

        public string GrupoDner { get; set; }

        public GrupoCusto()
        {
        }

        public override string ToString()
        {
            return CodGis + " - " + DescGis;
        }
    }
}
