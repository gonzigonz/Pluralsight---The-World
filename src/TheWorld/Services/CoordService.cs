using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class CoordService
    {
		private ILogger<CoordService> _logger;

		public CoordService(ILogger<CoordService> logger)
		{
			_logger = logger;
		}

		public CoordServiceResult Lookup(string location)
		{
			var result = new CoordServiceResult()
			{
				Success = false,
				Message = "Undertermined failure while looking up coordinates"
			};

			// Lookup Coordinates
			var encodedName = WebUtility.UrlEncode(location);
			var bingKey = Startup.Configuration["AppSettings:BingKey"];
			var url = $"http://dev.virtualearth.net/REST/v1/Location?q={encodedName}&key={bingKey}";


			return result;
		}
    }
}
