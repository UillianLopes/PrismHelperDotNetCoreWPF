using System;
using System.Threading.Tasks;
using WPFApp.Domain.Abstracts;

namespace WPFApp.Domain.Contracts
{
    public interface IGenericRepository
    {
        Task<T> Get<T>(Guid id) where T : Entity;

        Task Insert<T>(T entity) where T : Entity;

        void Update<T>(T entity) where T : Entity;

        void Delete<T>(T entity) where T : Entity;
    }
}
