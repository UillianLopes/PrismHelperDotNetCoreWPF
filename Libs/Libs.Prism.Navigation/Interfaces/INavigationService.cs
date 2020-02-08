using System.Threading.Tasks;
using System.Windows.Controls;

namespace Libs.Prism.Navigation.Interfaces
{
    public interface INavigationService
    {
        void Previous(string areaName, bool pop = false);

        void Next(string areaName, bool pop = false);

        void Pop(string areaName);

        void AddArea(string areaName, Frame frame);

        Task Navigate(string areaName, string route, bool useHistory = true, object param = null);
    }
}
