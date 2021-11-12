using System;

namespace OrcaFascio.Entity
{
    public abstract class EntityBase
    {
        public string Usuario { get =>"mestre"; }
        public DateTime DataAtual { get => DateTime.Now; }

    }
}
