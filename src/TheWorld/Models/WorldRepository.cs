using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheWorld.Models
{
	public class WorldRepository : IWorldRepository
	{
		private WorldContext _context;
		private ILogger<WorldRepository> _logger;

		public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
		{
			_context = context;
			_logger = logger;
		}

		public IEnumerable<Trip> GetAllTrips()
		{
			try
			{
				return _context.Trips
				.OrderBy(t => t.Name)
				.ToList();
			}
			catch (Exception e)
			{
				_logger.LogError("Could not get trips from database", e);
				return null;
			}
			
		}

		public IEnumerable<Trip> GetAllTripsWithStops()
		{
			try
			{ 
				return _context.Trips
					.Include(t =>t.Stops)
					.OrderBy(t => t.Name)
					.ToList();
			}
			catch (Exception e)
			{
				_logger.LogError("Could not get trips with stops from database", e);
				return null;
			}

}
	}
}
