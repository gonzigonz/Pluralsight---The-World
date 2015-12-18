using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace TheWorld.Controllers.Api
{
    public class TripController : Controller
    {
        [HttpGet("api/trips")]
        public JsonResult Get()
        {
			return Json(new { name = "Gonz" });
        }
    }
}
