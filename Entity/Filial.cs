using System;
using System.Collections.Generic;
using System.Text;

namespace OrcaFascio.Entity
{
    public class Filial : EntityBase
    {
        public int CodColigada { get; set; }

        public int CodFilial { get; set; }

        public string Cgc { get; set; }

        public string NomeFantasia { get; set; }

        public Filial(int codColigada, int codFilial, string cgc, string nomeFantasia)
        {
            CodColigada = codColigada;
            CodFilial = codFilial;
            Cgc = cgc;
            NomeFantasia = nomeFantasia;
        }

        public Filial()
        {
        }

        public override string ToString()
        {
            return CodFilial + " - " + NomeFantasia;
        }
    }
}
