using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VehicleRunsheetMBA.Data;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Data.Repositories
{
    public class RunsheetRepository : BaseRepository<Runsheet>, IRunsheetRepository
    {
        public ApplicationDbContext Context => _context as ApplicationDbContext;

        public RunsheetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Runsheet>> GetAllWithChildren()
        {
            return await _context.Runsheets.Include(x => x.Trips).ThenInclude(x => x.Orders).ToListAsync();
        }

        public async Task<Runsheet> GetByIdWithChildren(int id)
        {
            return await _context.Runsheets.Include(x => x.Trips).ThenInclude(x => x.Orders).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}