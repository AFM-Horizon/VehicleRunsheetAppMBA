using System;

namespace VehicleRunsheetMBAProj.Utilities
{
    public interface ILocalTimeUtility
    {
        DateTime ConvertToLocalTime(DateTime utcDateTime);

        DateTime ConvertToUtcTime(DateTime localDateTime);

        DateTime GetLocalTime();
    }
}