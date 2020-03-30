using FlightCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication11.Controllers
{
    public class BasicUserAuthentication : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                        "you must send user name + pwd in basic authentication");
                return;
            }
            string basicAuthBase64Token = actionContext.Request.Headers.Authorization.Parameter;

            string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(basicAuthBase64Token)); // itay:12345

            string[] authParams = decodedString.Split(':');

            string username = authParams[0];
            string pwd = authParams[1];

            ILoginToken token = FlightCenterSystem.Login(username, pwd, out BaseFacade facade);

            // checked if facade/token is null

            // 1 examine the token
            //if (token is LoginToken<Administrator>)
            if (facade is AdminFacade)
            {
                // ok to go 
                // check if actionContext.Request.RequestUri -- is admin ?
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                                "User is not admin. please try again");
            }


        }
    }
}