using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Data.Repositories
{
    public interface IRunsheetRepository : IRepository<Runsheet>
    {
        Task<IEnumerable<Runsheet>> GetAllWithChildren();
        Task<Runsheet> GetByIdWithChildren(int id);
    }
}