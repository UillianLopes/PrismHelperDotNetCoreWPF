using System.Collections.Generic;

namespace Libs.Prism.Navigation.Options
{
    public class NavigationSnapshot
    {
        internal NavigationSnapshot(IDictionary<string, object> queryParams, string route)
        {
            QueryParams = queryParams;
            Route = route;           
        }

        public IDictionary<string, object> QueryParams { get; }

        public string Route { get; }
        public string AreaName { get; }
    }
}
