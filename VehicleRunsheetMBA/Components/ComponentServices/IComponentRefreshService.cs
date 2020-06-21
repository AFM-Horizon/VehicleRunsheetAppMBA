using System;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Components.ComponentServices
{
    public interface IComponentRefreshService
    {
        event Action RefreshRequested;
        void CallRefresh();
    }
}