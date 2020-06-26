using System;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Components.ComponentServices
{
    /// <summary>
    /// Provides a way to notify a parent component to refresh itself. 
    /// </summary>
    public class ComponentRefreshService : IComponentRefreshService
    {
        public event Action RefreshRequested;

        /// <summary>
        /// Provides a way to notify a parent to refresh itself. 
        /// </summary>
        public void CallRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
