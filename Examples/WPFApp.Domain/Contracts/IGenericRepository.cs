using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPFApp.Domain.Abstracts;

namespace WPFApp.Domain.Contracts
{
    public interface IGenericRepository
    {
        Task<List<T>> GetList<T>(IFilter<T> id) where T : Entity;

        Task<List<M>> GetList<T, M>(IFilter<T, M> id) where T : Entity where M : class;

        Task<T> Get<T>(IFilter<T> id) where T : Entity;

        Task<M> Get<T, M>(IFilter<T, M> id) where T : Entity where M : class;

        Task<T> Get<T>(Guid id) where T : Entity;

        Task Insert<T>(T entity) where T : Entity;

        void Update<T>(T entity) where T : Entity;

        void Delete<T>(T entity) where T : Entity;
    }


    public interface IFilter<T> where T : class
    {
        public IQueryable<T> Apply(IQueryable<T> query);
    }

    public interface IFilter<T, M> where T : class where M : class
    {
        public IQueryable<M> Apply(IQueryable<T> query);
    }
}
