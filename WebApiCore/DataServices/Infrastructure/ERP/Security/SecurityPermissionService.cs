using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using WebApiCore.DataModels.EntityModels.TestDB;

namespace DataServices.Infrastructure
{
    public class SecurityPermissionService
    {
        #region Variable declaration & initialization
        TestDBContext _ctx = null;
        #endregion

        #region All Methods
        /// <summary>
        /// This method returns an object from database as object with pagination using asynchronous operation by vmInvParameters class as parameter.
        /// </summary>
        /// <param name="cmnParam"></param>
        /// <returns></returns>
        public async Task<object> GetPermissionByRoleId(int roleId)
        {
            object result = null; int recordsTotal = 0;
            var listVmMenu = new List<VmSecurityMenu>(); 
            var listPermission = new List<SecurityPermission>();
            var listVmPermission = new List<VmSecurityPermission>();
            try
            {
                using (_ctx = new TestDBContext())
                {
                    //listMenu = await _ctx.SecurityMenu.OrderBy(a=>a.MenuId).ToListAsync();
                    listVmMenu = (from menu in _ctx.SecurityMenu
                                  join mod in _ctx.SecurityModule on menu.ModuleId equals mod.ModuleId
                                  select new VmSecurityMenu
                                  {
                                      MenuId = menu.MenuId,
                                      MenuName = menu.MenuName,
                                      ModuleId = menu.ModuleId,
                                      Controller = menu.Controller,
                                      ModuleName = mod.ModuleName
                                  }).ToList();
                    listPermission = await _ctx.SecurityPermission.Where(w => w.RoleId == roleId).OrderBy(a => a.MenuId).ToListAsync();
                    foreach (var objVmMenu in listVmMenu)
                    {
                        var objVmPermission = new VmSecurityPermission();
                        var objPermission = (from P in listPermission where P.MenuId == objVmMenu.MenuId select P).FirstOrDefault();
                        if (objPermission != null)
                        {
                            objVmPermission.IsCreate = objPermission.IsCreate;
                            objVmPermission.IsRead = objPermission.IsRead;
                            objVmPermission.IsUpdate = objPermission.IsUpdate;
                            objVmPermission.IsDelete = objPermission.IsDelete;
                            objVmPermission.PermissionId = objPermission.PermissionId;
                        }
                        objVmPermission.Controller = objVmMenu.Controller;
                        objVmPermission.MenuId = objVmMenu.MenuId;
                        objVmPermission.MenuName = objVmMenu.MenuName;
                        objVmPermission.ModuleId = objVmMenu.ModuleId;
                        objVmPermission.ModuleName = objVmMenu.ModuleName;
                        objVmPermission.RoleId = roleId;
                        
                        listVmPermission.Add(objVmPermission);
                    }
                }
            }
            catch (Exception ex)
            {
                //Logs.WriteBug(ex);
            }

            return result = new
            {
                listVmPermission,
                recordsTotal
            };
        }
        
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
            SecurityPermission objPermission = null; object result = null;
            try
            {
                using (_ctx = new TestDBContext())
                {
                    objPermission = await _ctx.SecurityPermission.Where(x => x.PermissionId == id).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                //Logs.WriteBug(ex);
            }

            return result = new
            {
                objPermission
            };

        }


        /// <summary>
        /// Both insert and update can perform by BizIspPopzone model in database using asynchronous operation. when id is more than 0 update is performed otherwise insert. it returns an object as status of success or failure.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<object> SaveUpdate(VmSecurityPermission model)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            var objPermission = new SecurityPermission();
            using (_ctx = new TestDBContext())
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (model.PermissionId > 0) // update
                        {
                            objPermission = await _ctx.SecurityPermission.Where(x => x.PermissionId == model.PermissionId).FirstOrDefaultAsync();

                            objPermission.Controller = model.Controller;
                            objPermission.IsCreate = model.IsCreate;
                            objPermission.IsDelete = model.IsDelete;
                            objPermission.IsRead = model.IsRead;
                            objPermission.IsUpdate = model.IsUpdate;
                            objPermission.MenuId = model.MenuId;
                            objPermission.MenuName = model.MenuName;
                            objPermission.ModuleId = model.ModuleId;
                            objPermission.PermissionId = model.PermissionId;
                            objPermission.RoleId = model.RoleId;
                        }
                        else
                        {
                            objPermission.Controller = model.Controller;
                            objPermission.IsCreate = model.IsCreate;
                            objPermission.IsDelete = model.IsDelete;
                            objPermission.IsRead = model.IsRead;
                            objPermission.IsUpdate = model.IsUpdate;
                            objPermission.MenuId = model.MenuId;
                            objPermission.MenuName = model.MenuName;
                            objPermission.ModuleId = model.ModuleId;
                            objPermission.PermissionId = model.PermissionId;
                            objPermission.RoleId = model.RoleId;

                            await _ctx.SecurityPermission.AddAsync(objPermission);
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

    }
}
