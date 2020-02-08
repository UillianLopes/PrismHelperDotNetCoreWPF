using System.Collections.Generic;

namespace Libs.Prism.Navigation.Interfaces
{
    public interface IResolvableNavigation
    {
        bool OnResolved(IDictionary<string, object> resolved);
    }
}
