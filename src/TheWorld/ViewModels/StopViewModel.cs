using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    public class StopViewModel
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
		public DateTime Arrival { get; set; }
	}
}
