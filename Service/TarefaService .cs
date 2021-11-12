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

            if (!string.IsNullOrWhiteSpace(tarefa.CodCmp))
            {
                Composicao composicao = ComposicaoRepository.GetByCodigo(tarefa.CodColigada, tarefa.IdPrj, tarefa.CodCmp);

                if (composicao != null)
                {
                    tarefa.IdCmp = composicao.IdCmp;
                    tarefa.Servico = 1;
                }
            }

            //Tarefas sem composicao vao ser consideradas como cotacao
            if (tarefa.IdCmp != null)
            {
                tarefa.UtilizarValor = 0;
                tarefa.Valor = null;
            }

            return Repository.Add(tarefa);
        }
    }
}