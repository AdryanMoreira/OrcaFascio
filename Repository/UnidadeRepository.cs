using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class UnidadeRepository : RepositoryBase<Unidade>
    {
        
        public override IEnumerable<Unidade> Get(Unidade unidade)
        {

            var query = @"
            SELECT
                TUND.CODUND,
                TUND.DESCRICAO,
                TUNDCFOCOLAB.CODUNDCFO
            FROM TUND
            INNER JOIN TUNDCFOCOLAB ON TUNDCFOCOLAB.CODUND = TUND.CODUND
            WHERE TUNDCFOCOLAB.CODCOLCFO = 1
            AND TUNDCFOCOLAB.CODCFO = '000045'";

            return base.Get(query, unidade);
        }

        public override Unidade GetById(string codUndCfo)
        {

            Unidade unidade = new Unidade {  CodUndCfo = codUndCfo };

            var query = $@"
            SELECT
                TUND.CODUND,
                TUND.DESCRICAO,
                TUNDCFOCOLAB.CODUNDCFO
            FROM TUND
            INNER JOIN TUNDCFOCOLAB ON TUNDCFOCOLAB.CODUND = TUND.CODUND
            WHERE TUNDCFOCOLAB.CODUNDCFO = @CODUNDCFO
            AND TUNDCFOCOLAB.CODCOLCFO = 1
            AND TUNDCFOCOLAB.CODCFO = '000045'";

            return base.GetById(query, unidade);
        }
    }
}
