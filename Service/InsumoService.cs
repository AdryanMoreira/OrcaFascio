using OrcaFascio.Repository;
using OrcaFascio.Entity;
using System.Collections.Generic;
using System;

namespace OrcaFascio.Service
{
    public class InsumoService : ServiceBase<Insumo>
    {
        private readonly InsumoRepository Repository;
        private readonly AutoincrementoService AutoincrementoService;

        public InsumoService(InsumoRepository repository, AutoincrementoService autoincrementoService) : base(repository)
        {
            Repository = repository;
            AutoincrementoService = autoincrementoService;
        }

        public override int Add(Insumo insumo)
        {
            InsumoPrc insumoPrc = new InsumoPrc();

            insumo.IdIsm = AutoincrementoService.ObterValorAtualEAtualizar(Autoincremento.INSUMO).ValAutoInc;
            insumo.IdIsmPrc = insumoPrc.IdIsmPrc = AutoincrementoService.ObterValorAtualEAtualizar(Autoincremento.INSUMOPRC).ValAutoInc;

            insumoPrc.CodColigada = insumo.CodColigada;
            insumoPrc.CodUnd = insumo.CodUnd;
            insumoPrc.DescIsmPrc = "Preco em " + insumo.DataAtual;
            insumoPrc.IdPrj = insumo.IdPrj;
            insumoPrc.Valor = insumo.Valor;

            return Repository.Add(insumo, insumoPrc);
        }
    }
}