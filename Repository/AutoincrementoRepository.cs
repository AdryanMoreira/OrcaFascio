using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class AutoincrementoRepository : RepositoryBase<Autoincremento>
    {
        public Autoincremento GetFirstOrDefault(string codigo)
        {
            var query = $@"
            SELECT
                GAUTOINC.CODAUTOINC,
                GAUTOINC.VALAUTOINC
            FROM GAUTOINC (NOLOCK)
            WHERE GAUTOINC.CODAUTOINC = '{codigo}'";

            return base.GetFirstOrDefault(query, new Autoincremento() { CodAutoInc = codigo });
        }

        public override int Update(Autoincremento param)
        {
            var query = $@"
            UPDATE GAUTOINC
            SET GAUTOINC.VALAUTOINC = '{param.ValAutoInc}'
            WHERE GAUTOINC.CODAUTOINC = '{param.CodAutoInc}'";

            return base.Update(query, param);
        }
    }
}