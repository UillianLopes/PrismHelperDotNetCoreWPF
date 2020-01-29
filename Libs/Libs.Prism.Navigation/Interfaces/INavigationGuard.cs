using Libs.Prism.Navigation.Options;
using System.Threading.Tasks;

namespace Libs.Prism.Navigation.Interfaces
{
    public interface INavigationGuard
    {
        Task<bool> CanActivate(NavigationSnapshot snapshot);
    }
}
