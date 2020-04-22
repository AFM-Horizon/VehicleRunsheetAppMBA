using VehicleRunsheetMBA.Data;
using VehicleRunsheetMBA.Models;

namespace VehicleRunsheetMBAProj.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public ApplicationDbContext Context => _context as ApplicationDbContext;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}