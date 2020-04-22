using System;
using System.Threading.Tasks;
using VehicleRunsheetMBA.Data;

namespace VehicleRunsheetMBAProj.Data.Repositories
{
    public interface IUnitOfWork
    {
        public ITripRepository Trips { get; }
        public IRunsheetRepository Runsheets { get; }
        public IOrderRepository Orders { get; }
    }
}