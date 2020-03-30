using FlightCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication11.Controllers
{
    [BasicAuthenticationAdmin]
    public class AdministratorController : ApiController
    {
        // 1
        AdminFacade adminFacade = new AdminFacade();

        private LoginToken<Administrator> GetLoginToken()
        {
            // with authentication
            LoginToken<Administrator> token = (LoginToken<Administrator>)Request.Properties["token"];
            return token;

            // without authentication
            //return new LoginToken<Administrator>(); // fill with sample data 
        }

        [HttpPost]
        [Route("api/administrator/addairline")]
        public IHttpActionResult AddAirline([FromBody] Airline airline)
        {
            try
            {
                // where do i get the facade?
                // 2
                //AdminFacade facade = (AdminFacade)Request.Properties["facade"];
                // where do i get the token?
                LoginToken<Administrator> token = GetLoginToken();

                adminFacade.CreateAirline(token, airline);
                

            }
            catch (AirlineAlreadyExistException e)
            {
                // return error of 400 family bad request? 
            }
            catch (Exception e)
            {
                // error 500 -- sql exception
            }
            return Ok();
        }
    }
}
