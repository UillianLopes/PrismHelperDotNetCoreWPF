using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WPFApp.Domain.Abstracts;
using WPFApp.Domain.Contracts;
using WPFApp.Infra.Data;

namespace WPFApp.Infra.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly WPFAppDataContext _context;

        public GenericRepository(WPFAppDataContext context)
        {
            _context = context;
        }

        public void Delete<T>(T entity) where T : Entity => _context.Remove(entity);

        public Task<T> Get<T>(Guid id) where T : Entity => _context.Set<T>().FirstOrDefaultAsync(ety => ety.Id == id);

        public async Task Insert<T>(T entity) where T : Entity => await _context.Set<T>().AddAsync(entity);

        public void Update<T>(T entity) where T : Entity => _context.Set<T>().Update(entity);
    }
}
