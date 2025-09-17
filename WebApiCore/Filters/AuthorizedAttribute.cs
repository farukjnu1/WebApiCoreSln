using DataServices;
using DataUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.DataServices.Utilities;

namespace WebApiCore.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class AuthorizedAttribute : Attribute, IAuthorizationFilter
    {
        //public tokenModel objModel = null;

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var controllerInfo = filterContext.ActionDescriptor as ControllerActionDescriptor;
            if (filterContext != null)
            {
                // HttpContext
                string method = filterContext.HttpContext.Request.Method;

                // http header values
                var authToken = filterContext.HttpContext.Request.Headers[StaticInfos.authToken].ToString();
                var userAgent = filterContext.HttpContext.Request.Headers[StaticInfos.userAgent].ToString();
                
                //string authorizedToken = string.Empty;
                //string userAgent = string.Empty;
                string controllerName = controllerInfo.ControllerName;
                string actionName = controllerInfo.ActionName;

                WebAuthorization webAuthorization = new WebAuthorization();
                if (!webAuthorization.IsAuthorize(authToken, method, userAgent))
                {
                    string message = "Unauthorize access.";
                    object result = new { message };
                    filterContext.Result = new ContentResult()
                    {
                        Content = Newtonsoft.Json.JsonConvert.SerializeObject(result),
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        ContentType = "application/json"
                    };
                }

            }
        }

        

    }
}
