using System;
using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{
	public class StopViewModel
    {
		public int Id { get; set; }

		[Required]
		[StringLength(25, MinimumLength =5)]
		public string Name { get; set; }

		public double Longitude { get; set; }
		public double Latitude { get; set; }

		[Required]
		public DateTime Arrival { get; set; }
	}
}
