using OrcaFascio.Repository;
using System.Collections.Generic;

namespace OrcaFascio.Service
{
    public class ServiceBase<TEntity> where TEntity : class
    {
        private readonly RepositoryBase<TEntity> Repositorio;

        public ServiceBase(RepositoryBase<TEntity> repositorio) => Repositorio = repositorio;

        public virtual IEnumerable<TEntity> Get(TEntity param) => Repositorio.Get(param);

        public virtual TEntity GetFirstOrDefault(TEntity param) => Repositorio.GetFirstOrDefault(param);

        public virtual TEntity GetById(int id) => Repositorio.GetById(id);

        public virtual int Add(TEntity param) => Repositorio.Add(param);

        public virtual int Delete(TEntity param) => Repositorio.Delete(param);

        public virtual int Update(TEntity param) => Repositorio.Update(param);
    }
}
