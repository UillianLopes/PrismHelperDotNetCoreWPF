using System.Threading.Tasks;
using WPFApp.Domain.Contracts;

namespace WPFApp.Infra.Data
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly WPFAppDataContext _context;

        public UnityOfWork(WPFAppDataContext context)
        {
            _context = context;
        }

        public void Commit() => _context.SaveChanges();

        public Task CommitAsync() => _context.SaveChangesAsync();

    }
}
