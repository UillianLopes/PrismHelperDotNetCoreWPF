﻿using System.Threading.Tasks;
using System.Windows.Controls;

namespace Libs.Prism.Interfaces
{
    internal interface INavigationArea
    {
        void Previous(bool pop = false);

        void Next(bool pop = false);

        void Pop();

        void SetFrame(Frame frame);

        Task Navigate(string route, bool useHistory = true, object param = null);
    }
}
