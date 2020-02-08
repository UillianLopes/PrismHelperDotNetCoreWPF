using System.Threading.Tasks;

namespace WPFApp.Domain.Contracts
{
    public interface IUnityOfWork
    {
        Task CommitAsync();

        void Commit();

    }
}
