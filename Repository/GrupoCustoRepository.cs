using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class GrupoCustoRepository : RepositoryBase<GrupoCusto>
    {
        
        public override IEnumerable<GrupoCusto> Get(GrupoCusto grupoCusto)
        {

            var query = @"
            SELECT
				CODCOLIGADA,
				IDPRJ,
				IDGIS,
				CODGIS,
				DESCGIS,
                GRUPODNER
			FROM MGIS";

            return base.Get(query, grupoCusto);
        }

        public GrupoCusto GetByDescription(int codColigada, int idPrj, string descGis)
        {

            GrupoCusto grupoCusto = new GrupoCusto { CodColigada = codColigada, IdPrj = idPrj,  DescGis = descGis };

            var query = $@"
            SELECT
				CODCOLIGADA,
				IDPRJ,
				IDGIS,
				CODGIS,
				DESCGIS,
                GRUPODNER
			FROM MGIS
            WHERE CODCOLIGADA = @CODCOLIGADA
            AND IDPRJ = @IDPRJ
            AND DESCGIS = @DESCGIS";

            return base.GetById(query, grupoCusto);
        }
    }
}
