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

		public void AddStop(string tripName, Stop newStop)
		{
			var theTrip = GetAllTripsByName(tripName);
			newStop.Order = theTrip.Stops.Any() ? theTrip.Stops.Max(s => s.Order) + 1 : 0;

			theTrip.Stops.Add(newStop);
			_context.Stops.Add(newStop);
		}

		public void AddTrip(Trip newTrip)
		{
			_context.Add(newTrip);
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

		public Trip GetAllTripsByName(string tripName)
		{
			return _context.Trips.Include(t => t.Stops)
				.Where(t => t.Name == tripName)
				.FirstOrDefault();
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

		public IEnumerable<Trip> GetUserTripsWithStops(string name)
		{
			try
			{
				return _context.Trips
					.Include(t => t.Stops)
					.Where(t => t.UserName == name)
					.OrderBy(t => t.Name)
					.ToList();
			}
			catch (Exception e)
			{
				_logger.LogError("Could not get trips with stops from database", e);
				return null;
			}
		}

		public bool SaveAll()
		{
			return _context.SaveChanges() > 0;
		}
	}
}
