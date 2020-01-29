using Libs.Prism.Navigation.Options;
using System.Threading.Tasks;

namespace Libs.Prism.Navigation.Interfaces
{

    public interface INavigationResolver
    {
        string Key { get; }

        Task<object> Resolve(NavigationSnapshot snapshot);
    }
}
