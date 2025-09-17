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
    public class DropdownController : ControllerBase
    {

        #region Variable Declaration & Initialization
        private SecurityRoleService _service = null;
        #endregion

        #region Constructor
        public DropdownController()
        {
            _service = new SecurityRoleService();
        }
        #endregion

        #region All Http Methods
        [HttpGet("[action]")]//, BasicAuthorization]
        public async Task<object> getroles()
        {
            object result = null; object resdata = null;
            try
            {
                resdata = await _service.GetRoles();
            }
            catch (Exception) { }
            return result = new
            {
                resdata
            };
        }
        #endregion

    }
}