namespace OrcaFascio.Entity
{
    public class Coligada : EntityBase
    {
        public int CodColigada { get; set; }

        public string Cgc { get; set; }

        public string NomeFantasia { get; set; }

        public Coligada(int codColigada, string cgc, string nomeFantasia)
        {
            CodColigada = codColigada;
            Cgc = cgc;
            NomeFantasia = nomeFantasia;
        }

        public Coligada()
        {
        }

        public override string ToString()
        {
            return CodColigada + " - " + NomeFantasia;
        }
    }
}
