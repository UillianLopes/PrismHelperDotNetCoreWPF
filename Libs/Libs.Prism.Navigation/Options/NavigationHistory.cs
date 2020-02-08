using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Libs.Prism.Navigation.Options
{
    public class NavigationHistory
    {
        private readonly Stack<NavigationHistoryItem> _previousItems;
        private readonly Stack<NavigationHistoryItem> _nextItems;

        public NavigationHistoryItem CurrentItem { private set; get; }


        internal NavigationHistory() 
        {
            _previousItems = new Stack<NavigationHistoryItem>();
            _nextItems = new Stack<NavigationHistoryItem>();
        }

        internal void Add(string route, Page page)
        {
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
}
