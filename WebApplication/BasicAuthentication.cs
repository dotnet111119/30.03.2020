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

namespace WebApplication11
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {

        [ThreadStatic]
        public static string username_ = null;

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // return only -- it means i approve!
            // create response -- will resolve in blocking!

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

            if (username.ToUpper() == "itay".ToUpper() && pwd == "1234")
            {
                actionContext.Request.Properties["username"] = username;
                username_ = username;

                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("itay"), null);

                //actionContext.Request.GetRequestContext().Principal

                // principle
                // claim
                return;
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                                            "Incorrect user-name or password. please try again");
        }
    }
}