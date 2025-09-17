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
    public class UserController : ControllerBase
    {

        #region Variable Declaration & Initialization
        private SecurityUserService _service = null;
        #endregion

        #region Constructor
        public UserController()
        {
            _service = new SecurityUserService();
        }
        #endregion

        #region Http Methods for CRUD
        [HttpGet("[action]"), AuthorizedAttribute]
        public async Task<object> getbypage([FromQuery] int pageNumber, int pageSize, string sort = "asc")
        {
            object result = null; object resdata = null;
            try
            {
                vmCmnParameters cmnParam = new vmCmnParameters();
                cmnParam.pageNumber = pageNumber;
                cmnParam.pageSize = pageSize;
                cmnParam.currentSort = sort;
                resdata = await _service.GetWithPage(cmnParam);
            }
            catch (Exception) { }
            return result = new
            {
                resdata
            };
        }

        
        [HttpGet("[action]")]//, BasicAuthorization]
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
        
        [HttpPost("[action]"),AuthorizedAttribute]
        public async Task<object> addnew([FromBody]object data)
        {
            object result = null; object resdata = null;
            try
            {
                var model = JsonConvert.DeserializeObject<VmSecurityUser>(data.ToString());
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
                var model = JsonConvert.DeserializeObject<VmSecurityUser>(data.ToString());
                if (model != null && model.UserId == id)
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