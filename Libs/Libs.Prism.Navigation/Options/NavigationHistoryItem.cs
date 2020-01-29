using System.Windows.Controls;

namespace Libs.Prism.Navigation.Options
{
    public class NavigationHistoryItem
    {
        public NavigationHistoryItem(Page page, string route)
        {
            Page = page;
            Route = route;
        }

        public Page Page { get; private set; }
        public string Route { get; private set; }
    }
}
