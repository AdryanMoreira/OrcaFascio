using System.Collections.Generic;
using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class ColigadaRepository : RepositoryBase<Coligada>
    {
        
        public override IEnumerable<Coligada> Get(Coligada param)
        {
            var query = @"
                SELECT
                CODCOLIGADA,
                CGC,
                NOMEFANTASIA
            FROM GCOLIGADA (NOLOCK)
            WHERE GCOLIGADA.CODCOLIGADA <> 0";

            return base.Get(query, param);
        }

        public override Coligada GetById(int codColigada)
        {

            Coligada coligada = new Coligada { CodColigada = codColigada };

            var query = @"
            SELECT
                CODCOLIGADA,
                CGC,
                NOMEFANTASIA
            FROM GCOLIGADA (NOLOCK)
            WHERE GCOLIGADA.CODCOLIGADA = @CODCOLIGADA";

            return base.GetById(query, coligada);
        }

    }
}
