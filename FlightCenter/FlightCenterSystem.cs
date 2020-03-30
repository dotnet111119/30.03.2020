using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenter
{
    public class FlightCenterSystem
    {
        public static ILoginToken Login(string username, string pwd, out BaseFacade facade)
        {
            facade = new AdminFacade();
            return new LoginToken<Administrator>();
        }
    }
}
