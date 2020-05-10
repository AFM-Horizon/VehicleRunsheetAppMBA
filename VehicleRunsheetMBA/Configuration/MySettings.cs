using System.Collections.Generic;

namespace VehicleRunsheetMBA.Configuration
{
    public class MySettings : IMySettings
    {
        public Dictionary<string, string> Secrets { get; set; }
    }
}