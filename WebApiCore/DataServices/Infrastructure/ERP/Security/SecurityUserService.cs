using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.ViewModels;
using DataUtilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiCore.DataModels.EntityModels.TestDB;

namespace DataServices.Infrastructure
{
    public class SecurityUserService
    {
        #region Variable declaration & initialization
        TestDBContext _ctx = null;
        #endregion

        #region CRUD Methods

        /// <summary>
        /// This method returns an object from database as object with pagination using asynchronous operation by vmInvParameters class as parameter.
        /// </summary>
        /// <param name="cmnParam"></param>
        /// <returns></returns>
        public async Task<object> GetWithPage(vmCmnParameters cmnParam)
        {
            var listUser = new List<VmSecurityUser>(); object result = null; int recordsTotal = 0;

            try
            {
                using (_ctx = new TestDBContext())
                {
                    if (cmnParam.currentSort == "desc")
                    {
                        listUser = (from U in _ctx.SecurityUser
                                    join R in _ctx.SecurityRole on U.RoleId equals R.RoleId
                                    select new VmSecurityUser
                                    {
                                        UserId = U.UserId,
                                        Username = U.Username,
                                        UserPass = U.UserPass,
                                        RoleId = U.RoleId,
                                        RoleName = R.RoleName
                                    })
                                    .OrderByDescending(x => x.UserId)
                                    .Skip(Conversions.Skip(cmnParam))
                                    .Take((int)cmnParam.pageSize)
                                    .ToList();

                        recordsTotal = await _ctx.SecurityUser.CountAsync();
                    }
                    else
                    {
                        listUser = (from U in _ctx.SecurityUser
                                    join R in _ctx.SecurityRole on U.RoleId equals R.RoleId
                                    select new VmSecurityUser
                                    {
                                        UserId = U.UserId,
                                        Username = U.Username,
                                        UserPass = U.UserPass,
                                        RoleId = U.RoleId,
                                        RoleName = R.RoleName
                                    })
                                    .OrderBy(x => x.UserId)
                                    .Skip(Conversions.Skip(cmnParam))
                                    .Take((int)cmnParam.pageSize)
                                    .ToList();
                        recordsTotal = await _ctx.SecurityUser.CountAsync();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                //Logs.WriteBug(ex);
            }

            return result = new
            {
                listUser,
                recordsTotal
            };
        }
        
        /// <summary>
        /// This method returns data from database as an object using asynchronous operation by an int parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<object> GetByID(int id)
        {
            SecurityUser objUser = null; object result = null;
            try
            {
                using (_ctx = new TestDBContext())
                {
                    objUser = await _ctx.SecurityUser.Where(x => x.UserId == id).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                //Logs.WriteBug(ex);
            }

            return result = new
            {
                objUser
            };

        }


        /// <summary>
        /// Both insert and update can perform by BizIspPopzone model in database using asynchronous operation. when id is more than 0 update is performed otherwise insert. it returns an object as status of success or failure.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<object> SaveUpdate(VmSecurityUser model)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            var objUser = new SecurityUser();
            using (_ctx = new TestDBContext())
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (model.UserId > 0) // update
                        {
                            objUser = await _ctx.SecurityUser.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                            objUser.Username = model.Username;
                            objUser.UserPass = model.UserPass;
                            objUser.RoleId = model.RoleId;
                        }
                        else
                        {
                            objUser.Username = model.Username;
                            objUser.UserPass = model.UserPass;
                            objUser.RoleId = model.RoleId;

                            await _ctx.SecurityUser.AddAsync(objUser);
                        }

                        await _ctx.SaveChangesAsync();

                        _ctxTransaction.Commit();
                        message = MessageConstants.Saved;
                        resstate = MessageConstants.SuccessState;
                    }
                    catch (Exception ex)
                    {
                        _ctxTransaction.Rollback();
                        //Logs.WriteBug(ex);
                        message = MessageConstants.SavedWarning;
                        resstate = MessageConstants.ErrorState;
                    }
                }
            }
            return result = new
            {
                message,
                resstate
            };
        }

        /// <summary>
        /// Delete can perform to table by int parameter in database using asynchronous operation. It returns an object as status of success or failure.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<object> Delete(int id)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            using (_ctx = new TestDBContext())
            {
                using (var _ctxTran = await _ctx.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (id > 0)
                        {
                            var delModel = await _ctx.SecurityUser.Where(x => x.RoleId == id).FirstOrDefaultAsync();
                            _ctx.SecurityUser.Remove(delModel);
                        }

                        await _ctx.SaveChangesAsync();
                        _ctxTran.Commit();
                        message = MessageConstants.Deleted;
                        resstate = MessageConstants.SuccessState;
                    }
                    catch (Exception ex)
                    {
                        _ctxTran.Rollback();
                        //Logs.WriteBug(ex);
                        message = MessageConstants.DeletedWarning;
                        resstate = MessageConstants.ErrorState;
                    }
                }
            }
            return result = new
            {
                message,
                resstate
            };
        }

        #endregion

        #region methods for login
        public async Task<object> Login(VmSecurityUser model)
        {
            object result = null; VmSecurityUser objVmSecurityUser = null; VmLogin loggedUser = null; object menu = null;
            try
            {
                using (_ctx = new TestDBContext())
                {
                    var objSecurityUser = await _ctx.SecurityUser.SingleOrDefaultAsync(x => x.Username == model.Username && x.UserPass == model.UserPass);
                    if (objSecurityUser != null)
                    {
                        objVmSecurityUser = new VmSecurityUser
                        {
                            UserId = objSecurityUser.UserId,
                            Username = objSecurityUser.Username,
                            LoginTime = DateTime.Now.AddMinutes(StaticInfos.TokenExpiry).ToString("yyyy-MM-dd HH:mm:ss")
                        };

                        var strVmSecurityUser = JsonConvert.SerializeObject(objVmSecurityUser);
                        var strToken = Conversions.Encryptdata(strVmSecurityUser);

                        loggedUser = new VmLogin
                        {
                            Username = objSecurityUser.Username,
                            Token = strToken
                        };

                        SecurityPermissionService oSecurityPermissionService = new SecurityPermissionService();
                        menu = await oSecurityPermissionService.GetPermissionByRoleId((int)objSecurityUser.RoleId);
                        //
                    }
                }
            }
            catch //(Exception ex)
            {
                //Logs.WriteBug(ex);
            }

            return result = new
            {
                loggedUser,
                menu
            };
        }
        #endregion

    }
}
