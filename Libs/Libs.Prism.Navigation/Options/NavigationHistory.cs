using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Libs.Prism.Navigation.Options
{
    public class NavigationHistory
    {
        private readonly List<NavigationHistoryItem> _previousItems;
        private readonly List<NavigationHistoryItem> _nextItems;


        public NavigationHistoryItem this[string route]
        {
            get => _previousItems
                .Union(_nextItems)
                .FirstOrDefault(rt => rt.Route == route);
        }

        public NavigationHistoryItem CurrentItem { private set; get; }


        internal NavigationHistory() 
        {
            _previousItems = new List<NavigationHistoryItem>();
            _nextItems = new List<NavigationHistoryItem>();
        }

        internal void Add(string route, Page page)
        {
            _previousItems.RemoveAll(rt => rt.Route == route);

            if (CurrentItem != null)
            {
                _previousItems.Push(CurrentItem);

                if (_nextItems.Count > 0)
                    _nextItems.Clear();
            }
            
            CurrentItem = new NavigationHistoryItem(page, route);
        }

        internal Page Previous(bool pop = false)
        {
            if (!_previousItems.Any())
                return null;

            if (!pop)
                _nextItems.Push(CurrentItem);
            
            var previousItem = _previousItems.Pop();

            CurrentItem = previousItem;

            return CurrentItem.Page;
        }

        internal Page Next(bool pop = false)
        {
            if (!_nextItems.Any())
                return null;

            if (!pop)
                _previousItems.Push(CurrentItem);

            var nextItem = _nextItems.Pop();

            CurrentItem = nextItem;

            return CurrentItem.Page;
        }

        internal void Pop()
        {
            CurrentItem = null;
        }
    }

    static class ListExtentions
    {
        public static T Pop<T>(this IList<T> list)
        {
            if (list.Count <= 0)
                return default;

            var item = list[0];

            list.RemoveAt(0);

            return item;
        }

        public static T Peek<T>(this IList<T> list) => list.Count > 0 ? list[0] : default;

        public static void Push<T>(this IList<T> list, T item) => list.Insert(0, item);


    }
}
