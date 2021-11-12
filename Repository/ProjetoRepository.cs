using System.Collections.Generic;
using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class ProjetoRepository : RepositoryBase<Projeto>
    {
        public IEnumerable<Projeto> GetByColigada(int codColigada)
        {

            Projeto projeto = new Projeto { CodColigada = codColigada };

            var query = @"
                SELECT
                MPRJ.CODCOLIGADA,
                MPRJ.CODFILIAL,
                MPRJ.IDPRJ,
                MPRJ.CODPRJ,
                MPRJ.DESCRICAO
                FROM MPRJ
                JOIN MPRJCOMPL ON MPRJCOMPL.CODCOLIGADA = MPRJ.CODCOLIGADA
                AND MPRJCOMPL.IDPRJ = MPRJ.IDPRJ
                WHERE MPRJ.CODCOLIGADA = @CODCOLIGADA                
                AND MPRJCOMPL.ORCAFASCIO = '01'";

            return base.Get(query, projeto);
        }

        public Projeto GetById(int codColigada, int idPrj)
        {

            Projeto projeto = new Projeto { CodColigada = codColigada, IdPrj = idPrj };

            var query = @"
                SELECT
                MPRJ.CODCOLIGADA,
                MPRJ.CODFILIAL,
                MPRJ.IDPRJ,
                MPRJ.CODPRJ,
                MPRJ.DESCRICAO
                FROM MPRJ
                JOIN MPRJCOMPL ON MPRJCOMPL.CODCOLIGADA = MPRJ.CODCOLIGADA
                AND MPRJCOMPL.IDPRJ = MPRJ.IDPRJ
                WHERE MPRJ.CODCOLIGADA = @CODCOLIGADA
                AND MPRJ.IDPRJ = @IDPRJ
                AND MPRJCOMPL.ORCAFASCIO = '01'";

            return base.GetById(query, projeto);
        }

        public int AjustarHierarquia(Projeto param)
        {

            string query = $@"
                UPDATE MTAREFA SET IDPAI = 
                CASE LEN(MTAREFA.CODTRF)
                WHEN 3 THEN MTAREFA.IDTRF
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
