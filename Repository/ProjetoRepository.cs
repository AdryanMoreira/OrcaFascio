using System.Collections.Generic;
using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class ProjetoRepository : RepositoryBase<Projeto>
    {
        
        public override IEnumerable<Projeto> Get(Projeto param)
        {
            var query = @"
                SELECT
                CODCOLIGADA,
                CODFILIAL,
                IDPRJ,
                CODPRJ,
                DESCRICAO
                FROM MPRJ";

            return base.Get(query, param);
        }

        public IEnumerable<Projeto> GetByFilial(int codColigada, int codFilial)
        {

            Projeto projeto = new Projeto { CodColigada = codColigada, CodFilial = codFilial };

            var query = @"
                SELECT
                CODCOLIGADA,
                CODFILIAL,
                IDPRJ,
                CODPRJ,
                DESCRICAO
                FROM MPRJ
                WHERE MPRJ.CODCOLIGADA = @CODCOLIGADA
                AND MPRJ.CODFILIAL = @CODFILIAL";

            return base.Get(query, projeto);
        }

        public Projeto GetById(int codColigada, int idPrj)
        {

            Projeto projeto = new Projeto { CodColigada = codColigada, IdPrj = idPrj };

            var query = @"
                SELECT
                CODCOLIGADA,
                CODFILIAL,
                IDPRJ,
                CODPRJ,
                DESCRICAO
                FROM MPRJ
                WHERE MPRJ.CODCOLIGADA = @CODCOLIGADA
                AND MPRJ.IDPRJ = @IDPRJ";

            return base.GetById(query, projeto);
        }

        public int AjustarHierarquia(Projeto param)
        {

            string query = $@"
                UPDATE MTAREFA SET IDPAI = 
                CASE LEN(MTAREFA.CODTRF)
                WHEN 3 THEN
                (
	                SELECT PAI.IDTRF
	                FROM MTAREFA PAI
	                WHERE PAI.CODCOLIGADA = MTAREFA.CODCOLIGADA 
	                AND PAI.IDPRJ = MTAREFA.IDPRJ
	                AND PAI.CODTRF = (
	                    SELECT MIN(M2.CODTRF)
	                    FROM MTAREFA M2 WHERE M2.CODCOLIGADA = PAI.CODCOLIGADA AND M2.IDPRJ = PAI.IDPRJ
	                )
                )
                ELSE
                (
	                SELECT PAI.IDTRF
	                FROM MTAREFA PAI
	                WHERE PAI.CODCOLIGADA = MTAREFA.CODCOLIGADA 
	                AND PAI.IDPRJ = MTAREFA.IDPRJ
	                AND PAI.CODTRF = SUBSTRING(MTAREFA.CODTRF, 1, LEN(MTAREFA.CODTRF)-3)
                )
                END
                FROM MTAREFA 
                WHERE MTAREFA.CODCOLIGADA = @CODCOLIGADA
                AND MTAREFA.IDPRJ = @IDPRJ";

            return base.Update(query, param);
        }


        public int LimparProjeto(Projeto param)
        {

            string query = $@"
            DELETE MTRFCOMPL WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            DELETE MTRFDESC WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            UPDATE MTAREFA SET IDPAI = NULL WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            DELETE MTAREFA WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            DELETE FROM MCMPCOMPL WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            DELETE FROM MRECCMP WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            DELETE FROM MCMP WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            UPDATE MISMPRC set IDISM = NULL WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            DELETE FROM MISMCOMPL WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            DELETE from MISMDESC WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            DELETE from MISM WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;

            DELETE MISMPRC WHERE CODCOLIGADA = @CODCOLIGADA AND IDPRJ = @IDPRJ;";

            return base.Update(query, param);
        }
    }
}
