using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.EntityFrameworkCore;
using VehicleRunsheetMBA.Models;

namespace VehicleRunsheetMBAProj.Data.Repositories
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        public ApplicationDbContext Context => _context as ApplicationDbContext;

        public TripRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<Trip> GetTripWithChildrenByRunsheetId(int runsheetId)
        {
            return _context.Trips.Include(x => x.Orders).FirstOrDefaultAsync(x => x.RunsheetId == runsheetId && x.InProgress);
        }
    }
}