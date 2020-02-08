using Libs.Prism.Interfaces;
using Libs.Prism.Navigation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Libs.Prism.Navigation.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IDictionary<string, INavigationArea> _areas;
        private readonly IServiceProvider _provider;

        public NavigationService(IServiceProvider provider)
        {
            _provider = provider;
            _areas = new Dictionary<string, INavigationArea>();
        }

        private INavigationArea GetArea(string areaName)
        {
            if (!(_areas[areaName] is INavigationArea area))
                throw new InvalidOperationException($"Area {areaName} not found!");

            return area;
        }

        public Task Navigate(string areaName, string route, bool useHistory = true, object param = null) => GetArea(areaName).Navigate(route, useHistory, param);

        public void Next(string areaName, bool pop = false) => GetArea(areaName).Next(pop);

        public void Previous(string areaName, bool pop = false) => GetArea(areaName).Previous(pop);

        public void AddArea(string areaName, Frame frame)
        {
            if (_areas.Any(dic => dic.Key == areaName))
                throw new InvalidOperationException($"Duplicated area name {areaName}!");

            var area = new NavigationArea(_provider);
            
            area.SetFrame(frame);

            _areas.Add(areaName, area);
        }

        public void Pop(string areaName)
        {
            if (!(GetArea(areaName) is NavigationArea area))
                throw new InvalidOperationException($"Invalid area name {areaName}!");

            area.Pop();
        }
    }
}
