namespace OrcaFascio.Entity
{
    public class Autoincremento : EntityBase
    {
        public string CodAutoInc { get; set; }

        public int ValAutoInc { get; set; }

        public const string INSUMO = "IDISM";
        public const string INSUMOPRC = "IDISMPRC";
        public const string COMPOSICAO = "IDCMP";
        public const string RECURSO = "IDREC";
        public const string TAREFA = "IDTRF";
    }
}