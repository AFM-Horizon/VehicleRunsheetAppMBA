using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Utilities
{
    public static class VehicleEnumToIconConverter
    {
        public static string GetEnumIcon(VehicleEnums.VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleEnums.VehicleType.Truck:
                    return "fa fa-truck";
                case VehicleEnums.VehicleType.Ute:
                    return "fa fa-car";
                case VehicleEnums.VehicleType.Car:
                    return "fa fa-car";
                case VehicleEnums.VehicleType.Scooter:
                    return "fa fa-motorcycle";
                case VehicleEnums.VehicleType.Van:
                    return "fa fa-bus";
                case VehicleEnums.VehicleType.Bicycle:
                    return "fa fa-bicycle";
                default:
                    throw new ArgumentOutOfRangeException(nameof(vehicleType), vehicleType, null);
            }
        }
    
    }
}
