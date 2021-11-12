using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OrcaFascio
{
    class Repositorio
    {

            protected readonly string ConnectionString;

            protected SqlConnection conexao;

            public Repositorio()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;
        }

        public DataSet getColigadas()
        {
            DataSet ds = null;

            using (conexao = new SqlConnection(ConnectionString))
            {
                try
                { 
                conexao.Open();
                string queryString = "SELECT CODCOLIGADA, NOME, CGC FROM GCOLIGADA WHERE CODCOLIGADA <> 0";
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, conexao);

                ds = new DataSet();
                adapter.Fill(ds, "Coligadas");

                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if(conexao.State == ConnectionState.Open)
                        conexao.Close();
                }
            }

            return ds;
        }

        public DataSet getFiliais(int codColigada)
        {
            DataSet ds = null;

            using (conexao = new SqlConnection(ConnectionString))
            {
                try
                {
                    conexao.Open();
                    string queryString = string.Format("SELECT CODCOLIGADA, CODFILIAL, NOME, CGC FROM GFILIAL WHERE CODCOLIGADA = {0}", codColigada);
                    SqlDataAdapter adapter = new SqlDataAdapter(queryString, conexao);

                    ds = new DataSet();
                    adapter.Fill(ds, "Filiais");

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conexao.State == ConnectionState.Open)
                        conexao.Close();
                }
            }

            return ds;
        }


        public DataSet getProjetos(int codColigada, int codFilial)
        {
            DataSet ds = null;

            using (conexao = new SqlConnection(ConnectionString))
            {
                try
                {
                    conexao.Open();
                    string queryString = string.Format("SELECT CODCOLIGADA, CODFILIAL, IDPRJ, CODPRJ, DESCRICAO FROM MPRJ WHERE CODCOLIGADA = {0} AND CODFILIAL = {1}", codColigada, codFilial);
                    SqlDataAdapter adapter = new SqlDataAdapter(queryString, conexao);

                    ds = new DataSet();
                    adapter.Fill(ds, "Projetos");

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conexao.State == ConnectionState.Open)
                        conexao.Close();
                }
            }

            return ds;
        }

        public string getDatabaseName()
        {
            String db = null;

            using (conexao = new SqlConnection(ConnectionString))
            {
                try
                {

                    conexao.Open();
                    db = conexao.Database;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conexao.State == ConnectionState.Open)
                        conexao.Close();
                }
            }

            return db;
        }

        public DataSet getUnidadeRM(string codUndCfo)
        {
            DataSet ds = null;

            using (conexao = new SqlConnection(ConnectionString))
            {
                try
                {
                    conexao.Open();
                    string queryString = string.Format("SELECT CODUND FROM TUNDCFOCOLAB WHERE CODUNDCFO = '{0}'", codUndCfo);
                    SqlDataAdapter adapter = new SqlDataAdapter(queryString, conexao);

                    ds = new DataSet();
                    adapter.Fill(ds, "Unidades");

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conexao.State == ConnectionState.Open)
                        conexao.Close();
                }
            }

            return ds;
        }

        public DataSet getGrupoCustoRM(int codColigada, int idPrj, string grupo)
        {
            DataSet ds = null;

            using (conexao = new SqlConnection(ConnectionString))
            {
                try
                {
                    conexao.Open();
                    string queryString = string.Format("SELECT IDGIS FROM MGIS WHERE CODCOLIGADA = {0} AND IDPRJ = {1} AND DESCGIS = '{2}'", codColigada, idPrj, grupo);
                    SqlDataAdapter adapter = new SqlDataAdapter(queryString, conexao);
                    
                    ds = new DataSet();
                    adapter.Fill(ds, "GruposCusto");

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conexao.State == ConnectionState.Open)
                        conexao.Close();
                }
            }

            return ds;
        }

    }
}