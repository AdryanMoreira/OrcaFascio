using System;
using System.Collections.Generic;
using System.Text;

namespace OrcaFascio.Entity
{
    public class Projeto : EntityBase
    {
        public int CodColigada { get; set; }

        public int CodFilial { get; set; }

        public string CodPrj { get; set; }

        public int IdPrj { get; set; }

        public string Descricao { get; set; }

        public List<Insumo> Insumos { get; set; } = new List<Insumo>();

        public List<Composicao> Composicoes { get; set; } = new List<Composicao>();

        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();

        public Projeto()
        {
        }

        public override string ToString()
        {
            return CodPrj + " - " + Descricao;
        }
    }
}
