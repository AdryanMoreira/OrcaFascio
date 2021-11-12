using Dapper;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;

namespace OrcaFascio.Repository
{
    public class RepositoryBase<TEntity> where TEntity : class
    {
        protected readonly string ConnectionString;

        protected SqlConnection conexao;

        public RepositoryBase()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;
        }

        public virtual IEnumerable<TEntity> Get(TEntity param) => throw new NotImplementedException();
        public virtual TEntity GetFirstOrDefault(TEntity param) => throw new NotImplementedException();

        public virtual TEntity GetById(int id) => throw new NotImplementedException();

        public virtual TEntity GetById(string id) => throw new NotImplementedException();
        public virtual int Update(TEntity param) => throw new NotImplementedException();
        public virtual int Delete(TEntity param) => throw new NotImplementedException();
        public virtual int Add(TEntity param) => throw new NotImplementedException();
        public virtual bool Any(TEntity param) => throw new NotImplementedException();

        public virtual IEnumerable<TEntity> Get(string query, TEntity param)
        {
            using (conexao = new SqlConnection(ConnectionString))
            {
                return conexao.Query<TEntity>(query, param);
            }
        }

        public virtual TEntity GetFirstOrDefault(string query, TEntity param)
        {
            using (conexao = new SqlConnection(ConnectionString))
            {
                return conexao.QueryFirstOrDefault<TEntity>(query, param);
            }
        }

        public virtual TEntity GetById(string query, TEntity param)
        {
            using (conexao = new SqlConnection(ConnectionString))
            {
                return conexao.QueryFirstOrDefault<TEntity>(query, param);
            }
        }

        public virtual int Update(string query, TEntity param)
        {
            using (conexao = new SqlConnection(ConnectionString))
            {
                return conexao.Execute(query, param);
            }
        }

        public virtual int Delete(string query, TEntity param)
        {
            using (conexao = new SqlConnection(ConnectionString))
            {
                return conexao.Execute(query, param);
            }
        }

        public virtual int Add(string query, TEntity param)
        {
            using (conexao = new SqlConnection(ConnectionString))
            {
                return conexao.Execute(query, param);
            }
        }

        public virtual bool Any(string query, TEntity param)
        {
            using (conexao = new SqlConnection(ConnectionString))
            {
                return conexao.ExecuteScalar<bool>(query, param);
            }
        }
        public virtual string GetDatabaseName()
        {
            using (conexao = new SqlConnection(ConnectionString))
            {
                return conexao.Database;
            }
        }

    }
}
