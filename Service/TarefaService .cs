using OrcaFascio.Repository;
using OrcaFascio.Entity;
using System.Collections.Generic;
using System;

namespace OrcaFascio.Service
{
    public class TarefaService : ServiceBase<Tarefa>
    {
        private readonly TarefaRepository Repository;
        private readonly ComposicaoRepository ComposicaoRepository;
        private readonly AutoincrementoService AutoincrementoService;

        public TarefaService(TarefaRepository repository, AutoincrementoService autoincrementoService) : base(repository)
        {
            Repository = repository;
            AutoincrementoService = autoincrementoService;
            ComposicaoRepository = new ComposicaoRepository();
        }

        public override int Add(Tarefa tarefa)
        {
            int i = 0;

            tarefa.IdTrf = AutoincrementoService.ObterValorAtualEAtualizar(Autoincremento.TAREFA+"-"+ tarefa.IdPrj).ValAutoInc;

            if(!string.IsNullOrWhiteSpace(tarefa.CodCmp))
            { 
                Composicao composicao = ComposicaoRepository.GetByCodigo(tarefa.CodColigada, tarefa.IdPrj, tarefa.CodCmp);

                if (composicao != null)
                { 
                    tarefa.IdCmp = composicao.IdCmp;
                    tarefa.Servico = 1;
                }
            }

            if (!string.IsNullOrWhiteSpace(tarefa.CodTrfPai))
            {
                Tarefa tarefaPai = Repository.GetByCodigo(tarefa.CodColigada, tarefa.IdPrj, tarefa.CodTrfPai);

                if(tarefaPai != null)
                    tarefa.IdPai = tarefaPai.IdTrf;

            }
            
            if (Repository.Add(tarefa) > 0 && tarefa.CodTrfPai == tarefa.CodTrf)
            {
                tarefa.IdPai = tarefa.IdTrf;
                i += Repository.Update(tarefa);
            }

            return i;
        }
    }
}