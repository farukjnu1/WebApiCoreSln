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
    public class SecurityRoleService
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
        public async Task<object> GetWithPage(vmCmnParameters cmnParam)
        {
            var listRole = new List<SecurityRole>(); object result = null; int recordsTotal = 0;

            try
            {
                using (_ctx = new TestDBContext())
                {
                    if (cmnParam.currentSort == "desc")
                    {
                        listRole = await _ctx.SecurityRole
                        .OrderByDescending(x => x.RoleId)
                        .Skip(Conversions.Skip(cmnParam))
                        .Take((int)cmnParam.pageSize)
                        .ToListAsync();
                        recordsTotal = await _ctx.SecurityRole.CountAsync();
                    }
                    else
                    {
                        listRole = await _ctx.SecurityRole
                        .OrderBy(x => x.RoleId)
                        .Skip(Conversions.Skip(cmnParam))
                        .Take((int)cmnParam.pageSize)
                        .ToListAsync();
                        recordsTotal = await _ctx.SecurityRole.CountAsync();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                //Logs.WriteBug(ex);
            }

            return result = new
            {
                listRole,
                recordsTotal
            };
        }

        /// <summary>
        /// This method returns an object from database as object with pagination using asynchronous operation by vmInvParameters class as parameter.
        /// </summary>
        /// <param name="cmnParam"></param>
        /// <returns></returns>
        public async Task<object> GetRoles()
        {
            var listRole = new List<SecurityRole>(); object result = null; 

            try
            {
                using (_ctx = new TestDBContext())
                {
                    listRole = await _ctx.SecurityRole
                        .OrderBy(x => x.RoleName)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                //Logs.WriteBug(ex);
            }

            return result = new
            {
                listRole
            };
        }

        /// <summary>
        /// This method returns data from database as an object using asynchronous operation by an int parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<object> GetByID(int id)
        {
            SecurityRole objRole = null; object result = null;
            try
            {
                using (_ctx = new TestDBContext())
                {
                    objRole = await _ctx.SecurityRole.Where(x => x.RoleId == id).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                //Logs.WriteBug(ex);
            }

            return result = new
            {
                objRole
            };

        }


        /// <summary>
        /// Both insert and update can perform by model in database using asynchronous operation. when id is more than 0 update is performed otherwise insert. it returns an object as status of success or failure.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<object> SaveUpdate(VmSecurityRole model)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            var objRole = new SecurityRole();
            using (_ctx = new TestDBContext())
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (model.RoleId > 0) // update
                        {
                            objRole = await _ctx.SecurityRole.Where(x => x.RoleId == model.RoleId).FirstOrDefaultAsync();

                            objRole.RoleName = model.RoleName;
                        }
                        else
                        {
                            objRole.RoleName = model.RoleName;

                            await _ctx.SecurityRole.AddAsync(objRole);
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
                            var delModel = await _ctx.SecurityRole.Where(x => x.RoleId == id).FirstOrDefaultAsync();
                            _ctx.SecurityRole.Remove(delModel);
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
