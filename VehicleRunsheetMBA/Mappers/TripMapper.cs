using VehicleRunsheetMBA.Models;

namespace VehicleRunsheetMBAProj.Mappers
{
    public static class TripMapper
    {
        public static Trip MapTrip(Trip mapFrom, Trip mapTo)
        {
            mapTo.Customer = mapFrom.Customer;
            mapTo.StartTime = mapFrom.StartTime;
            mapTo.EndTime = mapFrom.EndTime;
            mapTo.Id = mapFrom.Id;
            mapTo.Orders = mapFrom.Orders;
            mapTo.ReceivedBy = mapFrom.ReceivedBy;
            mapTo.RunsheetId = mapFrom.RunsheetId;
            mapTo.Runsheet = mapFrom.Runsheet;

            return mapTo;
        }
    }
}