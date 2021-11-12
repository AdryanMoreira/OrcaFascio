using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using OrcaFascio.Entity;

namespace OrcaFascio.Repository
{
    public class InsumoRepository : RepositoryBase<Insumo>
    {

        public int Add(Insumo insumo, InsumoPrc insumoPrc)
        {
            int i = 0;

            using (conexao = new SqlConnection(ConnectionString))
            {
                conexao.Open();
                using (SqlTransaction trans = conexao.BeginTransaction())
                {
                    
                    string query = $@"
                    INSERT INTO MISMPRC(
                        CODCOLIGADA,
                        CODMOEVALOR,
                        CODMOEVALORIMPRODUTIVO,
                        CODUND,
                        DATAATUALIZACAO,
                        DESCISMPRC,
                        IDISM,
                        IDISMPRC,
                        IDPRJ,
                        IDPRJPRC,
                        PRECOATIVO,
                        RECCREATEDBY,
                        RECCREATEDON,
                        RECMODIFIEDBY,
                        RECMODIFIEDON,
                        VALOR,
                        VALORCOMLEIS,
                        VALORIMPRODUTIVO
                    )
                    VALUES(
                        @CODCOLIGADA,
                        'R$',
                        'R$',
                        @CODUND,
                        @DATAATUAL,
                        UPPER(@DESCISMPRC),
                        @IDISM,
                        @IDISMPRC,
                        @IDPRJ,
                        @IDPRJ,
                        1,
                        @USUARIO,
                        @DATAATUAL,
                        @USUARIO,
                        @DATAATUAL,
                        @VALOR,
                        @VALOR,
                        0.0000
                    )";

                    conexao.Execute(query, insumoPrc, transaction: trans);

                    query = $@"
                    INSERT INTO MISM(
                    APLICFORMULA,
                    CODAPLIC,
                    CODCOLIGADA,
                    CODISM,
                    CODUND,
                    CUSTOUNITHORAEXTRA,
                    DESCISM,
                    FLAGFRACIONARIO,
                    GRUPODNER,
                    IDGIS,
                    IDISM,
                    IDISMPRC,
                    IDPRJ,
                    ISMDETALHADO,
                    JORNADA,
                    JORNADAPERIODO,
                    MAXIMOHORAEXTRA,
                    MINIMOHORAEXTRA,
                    PRAZORESSUP,
                    PRIORIDADECALC,
                    RECCREATEDBY,
                    RECCREATEDON,
                    RECMODIFIEDBY,
                    RECMODIFIEDON,
                    TIPO,
                    TIPOGERANECESSIDADE,
                    TIPOISMDERIVADO,
                    UTILIZADETISMCNT,
                    VALOR,
                    VALORIMPRODUTIVO,
                    VALORSEMLEIS)
                    VALUES(
                    'M',
                    'M',
                    @CODCOLIGADA,
                    LTRIM(RTRIM(@CODISM)),
                    @CODUND,
                    0,
                    UPPER(SUBSTRING(@DESCISM, 1, 250)),
                    0,
                    @GRUPODNER,
                    @IDGIS,
                    @IDISM,
                    @IDISMPRC,
                    @IDPRJ,
                    0,
                    0.00,
                    0.00,
                    0.0000,
                    0.0000,
                    0,
                    0,
                    @USUARIO,
                    @DATAATUAL,
                    @USUARIO,
                    @DATAATUAL,
                    0,
                    0,
                    0,
                    0,
                    @VALOR,
                    0.0000,
                    @VALOR
                    )";

                    i = conexao.Execute(query, insumo, transaction: trans);

                    insumoPrc.IdIsm = insumo.IdIsm;

                    query = $@"
                    UPDATE MISMPRC
                    SET IDISM = @IDISM
                    WHERE MISMPRC.CODCOLIGADA = @CODCOLIGADA
                    AND MISMPRC.IDPRJ = @IDPRJ
                    AND MISMPRC.IDISMPRC = @IDISMPRC";

                    conexao.Execute(query, insumoPrc, transaction: trans);

                    trans.Commit();
                    conexao.Close();
                    
                }
            }
            return i;
        }

        public override IEnumerable<Insumo> Get(Insumo Insumo)
        {

            var query = @"
            SELECT
                CODCOLIGADA,
                CODISM,
                CODUND,
                DESCISM,
                IDGIS,
                IDISM,
                IDISMPRC,
                IDPRJ,
                VALOR
            FROM MISM";

            return base.Get(query, Insumo);
        }

        public Insumo GetByCodigo(int codcoligada, int idPrj, string codIsm)
        {
            Insumo insumo = new Insumo { CodColigada = codcoligada, IdPrj = idPrj, CodIsm = codIsm };

            var query = @"
            SELECT
                CODCOLIGADA,
                CODISM,
                CODUND,
                DESCISM,
                IDGIS,
                IDISM,
                IDISMPRC,
                IDPRJ,
                VALOR
            FROM MISM
            WHERE CODCOLIGADA = @CODCOLIGADA
            AND IDPRJ = @IDPRJ
            AND CODISM = LTRIM(RTRIM(@CODISM))";

            return base.GetById(query, insumo);
        }
    }
}
