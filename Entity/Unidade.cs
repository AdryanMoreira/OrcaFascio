using System;
using System.Collections.Generic;
using System.Text;

namespace OrcaFascio.Entity
{
    public class Unidade : EntityBase
    {

        public string CodUnd { get; set; }

        public string Descricao { get; set; }

        public string CodUndCfo { get; set; }

        public Unidade(string codUnd, string descricao, string codUndCfo)
        {
            CodUnd = codUnd;
            Descricao = descricao;
            CodUndCfo = codUndCfo;
        }

        public Unidade()
        {
        }

        public override string ToString()
        {
            return CodUnd + " - " + Descricao;
            return CodUnd + " - " + Descricao2;
        }
    }
}
