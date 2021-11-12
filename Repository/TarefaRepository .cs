using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class TarefaRepository : RepositoryBase<Tarefa>
    {
        public override int Add(Tarefa tarefa)
        {
            int i = 0;

            using (conexao = new SqlConnection(ConnectionString))
            {
                conexao.Open();
                using (SqlTransaction trans = conexao.BeginTransaction())
                {
                    string query = $@"
                        INSERT INTO MTAREFA(
                        ATIVA,
                        CODCOLIGADA,
                        CODTRF,
                        CODUND,
                        CUSTOUNIT,
                        DESCRICAO,
                        IDCMP,
                        IDPAI,
                        IDPRJ,
                        IDPRJREC,
                        IDTRF,
                        NOME,
                        QUANTIDADE,
                        RATEADO,
                        RECCREATEDBY,
                        RECCREATEDON,
                        SERVICO
                    )
                    VALUES(
                        1,
                        @CODCOLIGADA,
                        @CODTRF,
                        @CODUND,
                        @CUSTOUNIT,
                        UPPER(SUBSTRING(@DESCRICAO, 1, 253)),
                        @IDCMP,
                        @IDPAI,
                        @IDPRJ,
                        @IDPRJ,
                        @IDTRF,
                        UPPER(SUBSTRING(@NOME, 1, 89)),
                        @QUANTIDADE,
                        0,
                        @USUARIO,
                        @DATAATUAL,
                        @SERVICO
                    )";

                    i = conexao.Execute(query, tarefa, transaction: trans);

                    trans.Commit();
                    conexao.Close();
                    
                }
            }
            return i;
        }

        public Tarefa GetByCodigo(int codcoligada, int idPrj, string codTrf)
        {
            Tarefa tarefa = new Tarefa { CodColigada = codcoligada, IdPrj = idPrj, CodTrf = codTrf };

            var query = @"
            SELECT
                CODCOLIGADA,
                CODTRF,
                CODUND,
                CUSTOUNIT,
                DESCRICAO,
                IDCMP,
                IDPAI,
                IDPRJ,
                IDTRF,
                NOME,
                QUANTIDADE,
                SERVICO
            FROM MTAREFA
            WHERE CODCOLIGADA = @CODCOLIGADA
            AND IDPRJ = @IDPRJ
            AND CODTRF = LTRIM(RTRIM(@CODTRF))";

            return base.GetById(query, tarefa);
        }

        public override int Update(Tarefa tarefa)
        {
            var query = $@"
            UPDATE MTAREFA
            SET MTAREFA.IDPAI = @IDPAI
            WHERE MTAREFA.CODCOLIGADA = @CODCOLIGADA
            AND MTAREFA.IDTRF = @IDTRF";

            return base.Update(query, tarefa);
        }
    }
}
