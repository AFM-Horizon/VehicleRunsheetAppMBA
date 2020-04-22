using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRunsheetMBA.Data;

namespace VehicleRunsheetMBAProj.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            ITripRepository tripRepository,
            IRunsheetRepository runsheetRepository,
            IOrderRepository orderRepository)
        {
            Runsheets = runsheetRepository;
            Trips = tripRepository;
            Orders = orderRepository;
        }

        public ITripRepository Trips { get; }
        public IRunsheetRepository Runsheets { get; }
        public IOrderRepository Orders { get; }
    }
}
