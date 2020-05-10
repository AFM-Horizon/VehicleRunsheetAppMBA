using System.Collections.Generic;

namespace VehicleRunsheetMBA.Configuration
{
    public interface IMySettings
    {
        Dictionary<string,string> Secrets { get; set; }
    }
}