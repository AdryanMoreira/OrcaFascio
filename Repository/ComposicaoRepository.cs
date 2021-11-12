using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class ComposicaoRepository : RepositoryBase<Composicao>
    {
        public override int Add(Composicao composicao)
        {
            int i = 0;

            using (conexao = new SqlConnection(ConnectionString))
            {
                conexao.Open();
                using (SqlTransaction trans = conexao.BeginTransaction())
                {
                    
                    string query = $@"
                    INSERT INTO MCMP(
                        CODCMP,
                        CODCOLIGADA,
                        CODUND,
                        COMPOSICAOSISTEMA,
                        CUSTOIMPRODUTIVO,
                        CUSTOPRODUTIVO,
                        DESCCMP,
                        FATORKCMP,
                        FLAGTIPOCMP,
                        IDCMP,
                        IDPRJ,
                        RECCREATEDBY,
                        RECCREATEDON,
                        TIPOCOMPOSICAO,
                        VALORBDI,
                        VALORCOMLEI,
                        VALORSEMLEI,
                        VALORTOTAL
                    )
                    VALUES(
                        LTRIM(RTRIM(@CODCMP)),
                        @CODCOLIGADA,
                        @CODUND,
                        0,
                        0.0000,
                        0.0000,
                        UPPER(SUBSTRING(@DESCCMP, 1, 250)),
                        1.0000,
                        'N',
                        @IDCMP,
                        @IDPRJ,
                        @USUARIO,
                        @DATAATUAL,
                        0,
                        @VALORBDI,
                        @VALORTOTAL,
                        @VALORTOTAL,
                        @VALORTOTAL
                    )";

                    i = conexao.Execute(query, composicao, transaction: trans);

                    trans.Commit();
                    conexao.Close();
                    
                }
            }
            return i;
        }

        public Composicao GetByCodigo(int codcoligada, int idPrj, string codCmp)
        {
            Composicao composicao = new Composicao { CodColigada = codcoligada, IdPrj = idPrj, CodCmp = codCmp };

            var query = @"
            SELECT
                CODCMP,
                CODCOLIGADA,
                CODUND,
                DESCCMP,
                IDCMP,
                IDPRJ,
                VALORBDI,
                VALORTOTAL
            FROM MCMP
            WHERE CODCOLIGADA = @CODCOLIGADA
            AND IDPRJ = @IDPRJ
            AND CODCMP = LTRIM(RTRIM(@CODCMP))";

            return base.GetById(query, composicao);
        }
    }
}
