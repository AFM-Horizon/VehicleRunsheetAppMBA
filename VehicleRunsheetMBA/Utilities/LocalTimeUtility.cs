using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace VehicleRunsheetMBAProj.Utilities
{
    /// <summary>
    /// A utility that facilitates conversion between Local Time and U.T.C Time
    /// </summary>
    public static class LocalTimeUtility
    {
        private static DateTime ConvertToLocalTime(DateTime utcDateTime)
        {
            TimeZoneInfo aestZone = TimeZoneInfo.FindSystemTimeZoneById("Australia/Melbourne");
            //TimeZoneInfo aestZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
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

        /// <summary>
        /// Returns a <see cref="DateTime"/> equivalent of the U.T.C time received from the Azure Server
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLocalTime()
        {
            DateTime utcTime = DateTime.UtcNow;
            var finalTime = ConvertToLocalTime(utcTime);
            return finalTime;
        }
    }
}
