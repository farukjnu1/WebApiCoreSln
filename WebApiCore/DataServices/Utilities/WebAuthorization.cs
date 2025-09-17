using DataModels.ViewModels;
using DataServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.DataServices.Utilities
{
    public class WebAuthorization
    {
        public bool IsAuthorize(string authorizedToken, string httpMethod, string userAgent)
        {
            bool result = false;
            try
            {
                var strToken = Conversions.Decryptdata(authorizedToken);
                var oVmSecurityUser = JsonConvert.DeserializeObject<VmSecurityUser>(strToken);
                var loginTime = Convert.ToDateTime(oVmSecurityUser.LoginTime);
                if (loginTime > DateTime.Now)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
