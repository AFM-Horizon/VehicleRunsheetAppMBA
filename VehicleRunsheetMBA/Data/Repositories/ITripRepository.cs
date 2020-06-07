using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRunsheetMBA.Models;

namespace VehicleRunsheetMBAProj.Data.Repositories
{
    public interface ITripRepository : IRepository<Trip>
    {
        Task<Trip> GetTripWithChildrenByRunsheetId(int runsheetId);
    }
}