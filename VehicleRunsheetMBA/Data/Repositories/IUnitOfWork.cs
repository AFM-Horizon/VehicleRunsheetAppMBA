using System;
using System.Threading.Tasks;

namespace VehicleRunsheetMBAProj.Data.Repositories
{
    public interface IUnitOfWork
    {
        public ITripRepository Trips { get; }
        public IRunsheetRepository Runsheets { get; }
        public IOrderRepository Orders { get; }
        public IVehicleRepository Vehicles { get; set; }
    }
}