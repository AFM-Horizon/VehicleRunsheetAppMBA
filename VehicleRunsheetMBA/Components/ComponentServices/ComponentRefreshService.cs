using System;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Components.ComponentServices
{
    public class ComponentRefreshService : IComponentRefreshService
    {
        public event Action RefreshRequested;

        public void CallRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
