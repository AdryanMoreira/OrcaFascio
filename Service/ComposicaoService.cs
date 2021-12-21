using OrcaFascio.Repository;
using OrcaFascio.Entity;
using System.Collections.Generic;
using System;

namespace OrcaFascio.Service
{
    public class ComposicaoService : ServiceBase<Composicao>
    {
        
        private readonly ComposicaoRepository Repository;
        private readonly AutoincrementoService AutoincrementoService;

        public ComposicaoService(ComposicaoRepository repository, AutoincrementoService autoincrementoService) : base(repository)
        {
            Repository = repository;
            AutoincrementoService = autoincrementoService;
        }

        public override int Add(Composicao composicao)
        {
            composicao.IdCmp = AutoincrementoService.ObterValorAtualEAtualizar(Autoincremento.COMPOSICAO).ValAutoInc;

            return Repository.Add(composicao);
        }
    }
}