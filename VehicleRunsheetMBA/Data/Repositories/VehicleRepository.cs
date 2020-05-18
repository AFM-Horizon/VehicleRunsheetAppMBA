using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Data.Repositories
{
    public class VehicleRepository : BaseRepository<VehicleDetails>, IVehicleRepository
    {
        public ApplicationDbContext Context => _context as ApplicationDbContext;

        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

