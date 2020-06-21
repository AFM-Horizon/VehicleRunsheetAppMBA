using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace VehicleRunsheetMBAProj.Utilities
{
    public static class LocalTimeUtility
    {
        public static DateTime ConvertToLocalTime(DateTime utcDateTime)
        {
            Console.WriteLine("Convert To Local Time");
            TimeZoneInfo aestZone = TimeZoneInfo.FindSystemTimeZoneById("Australia/Melbourne");
            //TimeZoneInfo aestZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            Console.WriteLine(aestZone.BaseUtcOffset.ToString());

            DateTime aestTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, aestZone);
            Console.WriteLine(aestTime.ToShortTimeString());

            return aestTime;
        }

        public static DateTime ConvertToUtcTime(DateTime localDateTime)
        {
            Console.WriteLine("Convert To Utc Time");
            Console.WriteLine("Local: " + localDateTime.ToShortTimeString());
            var output = localDateTime.ToUniversalTime();
            Console.WriteLine("UTC: " + output.ToShortTimeString());
            return output;
        }

        public static DateTime GetLocalTime()
        {
            Console.WriteLine("Get Local Time");
            DateTime utcTime = DateTime.UtcNow;
            Console.WriteLine("UTC Time: " + utcTime.ToShortTimeString());
            var finalTime = ConvertToLocalTime(utcTime);
            Console.WriteLine("Final Time: " + finalTime.ToShortTimeString());
            return finalTime;
        }
    }
}
