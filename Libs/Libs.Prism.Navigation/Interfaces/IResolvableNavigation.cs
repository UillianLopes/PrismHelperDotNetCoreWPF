﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libs.Prism.Navigation.Interfaces
{
    public interface IResolvableNavigation
    {
        void OnResolved(IDictionary<string, object> resolved);
    }
}
