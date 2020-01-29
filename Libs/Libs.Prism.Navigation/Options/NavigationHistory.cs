using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Libs.Prism.Navigation.Options
{
    public class NavigationHistory
    {
        private readonly IList<NavigationHistoryItem> _items;

        public string CurrentRoute { get; private set; }

        public NavigationHistoryItem this[string route] => 
            _items.FirstOrDefault(itm => itm.Route == route);

        internal NavigationHistory() => _items = new List<NavigationHistoryItem>();

        internal void Add(string route, Page page)
        {
            if (_items.FirstOrDefault(rt => rt.Route == route) is NavigationHistoryItem item)
            {
                _items.Remove(item);
                _items.Add(item);
                return;
            }

            _items.Add(new NavigationHistoryItem(page, route));
            CurrentRoute = route;
        }

        internal Page Previous()
        {
            if (!(_items.FirstOrDefault(rt => rt.Route == CurrentRoute) is NavigationHistoryItem currentItem))
                throw new InvalidOperationException("Invalid route");

            var index = _items
                .IndexOf(currentItem);

            if (index <= 0)
                return currentItem.Page;

            if (!(_items.ElementAt(--index) is NavigationHistoryItem previousItem))
                return currentItem.Page;

            CurrentRoute = previousItem.Route;

            return previousItem.Page;
        }

        internal Page Next()
        {
            if (!(this[CurrentRoute] is NavigationHistoryItem currentItem))
                return null;

            var index = _items.IndexOf(currentItem);

            if (index < (_items.Count - 1))
                return currentItem.Page;

            if (!(_items.ElementAt(++index) is NavigationHistoryItem nextItem))
                return currentItem.Page;

            CurrentRoute = nextItem.Route;

            return nextItem.Page;
        }
    }
}
