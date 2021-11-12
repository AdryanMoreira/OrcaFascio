using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class RecursoRepository : RepositoryBase<Recurso>
    {
        public override int Add(Recurso recurso)
        {
            int i = 0;

            using (conexao = new SqlConnection(ConnectionString))
            {
                conexao.Open();
                using (SqlTransaction trans = conexao.BeginTransaction())
                {

					string query = $@"
					INSERT INTO MRECCMP(
						ATIVO,
						CODCOLIGADA,
						COEFICIENTEIMPRODUTIVO,
						COEFICIENTEPRODUTIVO,
						DMT,
						DMTR,
						DMTT,
						EQUIPE,
						IDCMP,
						IDCMPFILHA,
						IDISM,
						IDPRJ,
						IDPRJREC,
						IDREC,
						QUANTIDADE,
						RECCREATEDBY,
						RECCREATEDON,
						VALORTOTAL,
						VALORUNIT
					)
					VALUES(
						1,
						@CODCOLIGADA,
						0.0000,
						10000.0000,
						10000.0000,
						0.0000,
						0.0000,
						1.0000,
						@IDCMP,
						@IDCMPFILHA,
						@IDISM,
						@IDPRJ,
						@IDPRJ,
						@IDREC,
						@QUANTIDADE * 10000,
						@USUARIO,
						@DATAATUAL,
						@VALORTOTAL,
						@VALORUNIT)";

                    i = conexao.Execute(query, recurso, transaction: trans);

                    trans.Commit();
                    conexao.Close();
                }
            }
            return i;
        }
    }
}
