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
using WebApiCore.Filters;

namespace SchoolWebApi.api.Section
{
    //[Route("api/[controller]")]
    [Route("api/[controller]"), Produces("application/json"), EnableCors("AllowAll")]
    [ApiController]
    public class PermissionController : ControllerBase
    {

        #region Variable Declaration & Initialization
        private SecurityPermissionService _service = null;
        #endregion

        #region Constructor
        public PermissionController()
        {
            _service = new SecurityPermissionService();
        }
        #endregion

        #region All Http Methods
        [HttpGet("[action]"), AuthorizedAttribute]
        public async Task<object> getpermissionbyid([FromQuery] int id)
        {
            object result = null; object resdata = null;
            try
            {
                resdata = await _service.GetPermissionByRoleId(id);
            }
            catch (Exception) { }
            return result = new
            {
                resdata
            };
        }

        
        [HttpGet("[action]"), AuthorizedAttribute]
        public async Task<object> getbyid([FromQuery] int id)
        {
            object result = null; object resdata = null;
            try
            {
                resdata = await _service.GetByID(id);
            }
            catch (Exception) { }
            return result = new
            {
                resdata
            };
        }
        
        [HttpPost("[action]"), AuthorizedAttribute]
        public async Task<object> addnew([FromBody]object data)
        {
            object result = null; object resdata = null;
            try
            {
                var model = JsonConvert.DeserializeObject<VmSecurityPermission>(data.ToString());
                if (model != null)
                {
                    resdata = await _service.SaveUpdate(model);
                }
            }
            catch (Exception) { }

            return result = new
            {
                resdata
            };
        }

        [HttpPut("[action]"), AuthorizedAttribute]
        public async Task<object> update(int id, [FromBody]object data)
        {
            object result = null; object resdata = null;
            try
            {
                var model = JsonConvert.DeserializeObject<VmSecurityPermission>(data.ToString());
                if (model != null && model.PermissionId == id)
                {
                    resdata = await _service.SaveUpdate(model);
                }
            }
            catch (Exception) { }

            return result = new
            {
                resdata
            };
        }

        [HttpDelete("[action]"), AuthorizedAttribute]
        public async Task<object> delete([FromQuery] int id)
        {
            object result = null; object resdata = string.Empty;
            try
            {
                resdata = await _service.Delete(id);
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