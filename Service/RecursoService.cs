using OrcaFascio.Repository;
using OrcaFascio.Entity;
using System.Collections.Generic;
using System;

namespace OrcaFascio.Service
{
    public class RecursoService : ServiceBase<Recurso>
    {
        private readonly RecursoRepository Repository;
        private readonly InsumoRepository InsumoRepository;
        private readonly ComposicaoRepository ComposicaoRepository;
        private readonly AutoincrementoService AutoincrementoService;

        public RecursoService(RecursoRepository repository, AutoincrementoService autoincrementoService) : base(repository)
        {
            Repository = repository;
            AutoincrementoService = autoincrementoService;
            InsumoRepository = new InsumoRepository();
            ComposicaoRepository = new ComposicaoRepository();
        }

        public override int Add(Recurso recurso)
        {
            
            recurso.IdRec = AutoincrementoService.ObterValorAtualEAtualizar(Autoincremento.RECURSO).ValAutoInc;

            Composicao composicao = ComposicaoRepository.GetByCodigo(recurso.CodColigada, recurso.IdPrj, recurso.CodCmpPrincipal);

            if (composicao != null)
                recurso.IdCmp = composicao.IdCmp;

            if (recurso.CodCmpFilha != null)
            {                
                composicao = ComposicaoRepository.GetByCodigo(recurso.CodColigada, recurso.IdPrj, recurso.CodCmpFilha);

                if (composicao != null)
                    recurso.IdCmpFilha = composicao.IdCmp;
                
            }
            else if(recurso.CodIsm != null)
            {
                Insumo insumo = InsumoRepository.GetByCodigo(recurso.CodColigada, recurso.IdPrj, recurso.CodIsm);
                if(insumo != null)
                    recurso.IdIsm = insumo.IdIsm;
            }

            return Repository.Add(recurso);
        }
    }
}