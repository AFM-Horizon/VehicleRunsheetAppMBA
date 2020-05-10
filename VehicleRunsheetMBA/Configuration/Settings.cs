using Microsoft.Extensions.Options;

namespace VehicleRunsheetMBA.Configuration
{
    public class Settings
    {
        private readonly IOptions<MySettings> _mySettings;

        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }

        public Settings(IOptionsSnapshot<MySettings> mySettings)
        {
            _mySettings = mySettings;

            AdminEmail = _mySettings.Value.Secrets["AdminEmail"];
            AdminPassword = _mySettings.Value.Secrets["AdminPassword"];
        }
    }
}