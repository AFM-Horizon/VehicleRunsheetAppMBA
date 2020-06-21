using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRunsheetMBAProj.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            ITripRepository tripRepository,
            IRunsheetRepository runsheetRepository,
            IOrderRepository orderRepository,
            IVehicleRepository vehicleRepository)
        {
            Runsheets = runsheetRepository;
            Trips = tripRepository;
            Orders = orderRepository;
            Vehicles = vehicleRepository;
        }

        public ITripRepository Trips { get; }
        public IRunsheetRepository Runsheets { get; }
        public IOrderRepository Orders { get; }
        public IVehicleRepository Vehicles { get; set; }  
    }
}
