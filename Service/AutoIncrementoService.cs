using OrcaFascio.Entity;
using OrcaFascio.Repository;

namespace OrcaFascio.Service
{
    public class AutoincrementoService : ServiceBase<Autoincremento>
    {
        private readonly AutoincrementoRepository Repository;

        public AutoincrementoService(AutoincrementoRepository repository) : base(repository) => Repository = repository;

        public Autoincremento ObterValorAtualEAtualizar(string codigo, int quantidade = 1)
        {
            var autoincremento = Repository.GetFirstOrDefault(codigo);
            autoincremento.ValAutoInc += quantidade;
            Repository.Update(autoincremento);
            return autoincremento;
        }
    }
}