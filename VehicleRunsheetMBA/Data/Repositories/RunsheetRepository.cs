﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            List<Runsheet> outList = new List<Runsheet>();

            var list = await _context.Runsheets
                .Include(x => x.VehicleDetails)
                .Include(x => x.Trips)
                .ThenInclude(x => x.Orders)
                .ToListAsync();
            foreach (var entity in list)
            {
                await _context.Entry(entity).ReloadAsync();
            }

            return list;
        }

        public async Task<Runsheet> GetByIdWithChildren(int id)
        {
            return await _context.Runsheets
                .Include(x => x.VehicleDetails)
                .Include(x => x.Trips)
                .ThenInclude(x => x.Orders)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}