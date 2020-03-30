using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenter
{
    public abstract class BaseFacade
    {

    }
    public class AnonymousFacade : BaseFacade
    {
        public List<Flight> GetAllFlights()
        {
            return new List<Flight>();
        }
    }
    public class AdminFacade : BaseFacade
    {
        public void CreateAirline(LoginToken<Administrator> token, Airline airline)
        {

        }
    }
    public class AirlineFacade : BaseFacade
    {

    }
    public class CustomerFacade : BaseFacade
    {

    }
}
