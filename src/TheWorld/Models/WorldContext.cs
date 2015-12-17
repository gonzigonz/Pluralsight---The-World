using System;
using Microsoft.Data.Entity;

namespace TheWorld.Models
{
	public class WorldContext : DbContext
	{
		public DbSet<Trip> Trips { get; set; }
		public DbSet<Stop> Stops { get; set; }
	}
}