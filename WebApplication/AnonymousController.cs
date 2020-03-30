using FlightCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication11.Controllers
{
    public class AnonymousController : ApiController
    {

        private AnonymousFacade anonymousFacade = new AnonymousFacade();

        [HttpGet]
        [Route("api/anonymous/getflights/")]
        public IHttpActionResult GetAllFlights()
        {
            List<Flight> result = anonymousFacade.GetAllFlights();

            // return Ok(result);

            if (result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
