using System.Threading.Tasks;
using System.Windows.Controls;

namespace Libs.Prism.Navigation.Interfaces
{
    public interface INavigationService
    {
        void Previous(string areaName);

        void Next(string areaName);

        void AddArea(string areaName, Frame frame);

        Task Navigate(string areaName, string route, bool useHistory = true, object param = null);
    }
}
