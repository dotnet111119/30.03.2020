﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightCenter
{
    public interface ILoginToken
    {

    }
    public class LoginToken <T> : ILoginToken where T : IUser
    {
    }

}
