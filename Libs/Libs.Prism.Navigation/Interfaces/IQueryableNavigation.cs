using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libs.Prism.Navigation.Interfaces
{
    public interface IQueryableNavigation
    {
        Task OnQueried(IDictionary<string, object> query);
    }
}
