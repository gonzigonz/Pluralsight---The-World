using Microsoft.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace TheWorld.Models
{
	public class WorldRepository : IWorldRepository
	{
		private WorldContext _context;

		public WorldRepository(WorldContext context)
		{
			_context = context;
		}

		public IEnumerable<Trip> GetAllTrips()
		{
			return _context.Trips
				.OrderBy(t => t.Name)
				.ToList();
		}

		public IEnumerable<Trip> GetAllTripsWithStops()
		{
			return _context.Trips
				.Include(t =>t.Stops)
				.OrderBy(t => t.Name)
				.ToList();
		}
	}
}
