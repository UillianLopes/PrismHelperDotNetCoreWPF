using System;
using System.Windows.Controls;

namespace Libs.Prism.Navigation.Options
{
    public class NavigationHistoryItem : ICloneable
    {

        public NavigationHistoryItem(Page page, string route)
        {
            Page = page;
            Route = route;
        }

        public Page Page { get; private set; }
        public string Route { get; private set; }

        public object Clone() => new NavigationHistoryItem(Page, Route);
    }
}
