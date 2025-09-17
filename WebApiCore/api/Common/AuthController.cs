using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DataServices.Infrastructure;
using DataModels.ViewModels;

namespace SchoolWebApi.api.Section
{
    //[Route("api/[controller]")]
    [Route("api/[controller]"), Produces("application/json"), EnableCors("AllowAll")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        #region Variable Declaration & Initialization
        private SecurityUserService _service = null;
        #endregion

        #region Constructor
        public AuthController()
        {
            _service = new SecurityUserService();
        }
        #endregion
        
        #region All Http Methods
        // GET: api/auth/login
        [HttpPost("[action]")]
        public async Task<object> login()
        {
            object result = null; object resdata = null;
            try
            {
                var model = new VmSecurityUser()
                {
                    Username = Convert.ToString(Request.Form["userName"]),
                    UserPass = Convert.ToString(Request.Form["userPass"]),
                };
                
                if (model != null)
                {
                    resdata = await _service.Login(model);
                }
            }
            catch (Exception)
            {

            }
            return result = new
            {
                resdata
            };
        }
        #endregion

    }
}