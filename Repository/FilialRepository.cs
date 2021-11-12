using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class FilialRepository : RepositoryBase<Filial>
    {
        
        public override IEnumerable<Filial> Get(Filial filial)
        {

            var query = @"
                SELECT
                GFILIAL.CODFILIAL,
                GFILIAL.CGC,
                UPPER(GFILIAL.NOMEFANTASIA) NOMEFANTASIA,
                GFILIAL.CODCOLIGADA
            FROM GFILIAL (NOLOCK)
            WHERE GFILIAL.CODCOLIGADA <> 0";

            return base.Get(query, filial);
        }

        public IEnumerable<Filial> GetByColigada(int codColigada)
        {

            Filial filial = new Filial { CodColigada = codColigada };

            var query = @"
                SELECT
                GFILIAL.CODFILIAL,
                GFILIAL.CGC,
                UPPER(GFILIAL.NOMEFANTASIA) NOMEFANTASIA,
                GFILIAL.CODCOLIGADA
            FROM GFILIAL (NOLOCK)
            WHERE GFILIAL.CODCOLIGADA = @CODCOLIGADA";

            return base.Get(query, filial);
        }

        public Filial GetById(int codColigada, int codFilial)
        {

            Filial filial = new Filial { CodColigada = codColigada, CodFilial = codFilial };

            var query = $@"
            SELECT
	            CODCOLIGADA,
	            CODFILIAL,
	            CGC,
	            UPPER(NOMEFANTASIA) NOMEFANTASIA
            FROM GFILIAL (NOLOCK)
            WHERE GFILIAL.CODCOLIGADA = @CODCOLIGADA
            AND GFILIAL.CODFILIAL = @CODFILIAL";

            return base.GetById(query, filial);
        }
    }
}
