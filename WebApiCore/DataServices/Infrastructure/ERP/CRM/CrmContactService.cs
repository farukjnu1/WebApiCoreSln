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
    public class CrmContactService
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
            var listContact = new List<CrmContact>(); object result = null; int recordsTotal = 0;

            try
            {
                using (_ctx = new TestDBContext())
                {
                    if (cmnParam.currentSort == "desc")
                    {
                        listContact = await _ctx.CrmContact
                        .OrderByDescending(x => x.ContactId)
                        .Skip(Conversions.Skip(cmnParam))
                        .Take((int)cmnParam.pageSize)
                        .ToListAsync();
                        recordsTotal = await _ctx.CrmContact.CountAsync();
                    }
                    else
                    {
                        listContact = await _ctx.CrmContact
                        .OrderBy(x => x.ContactId)
                        .Skip(Conversions.Skip(cmnParam))
                        .Take((int)cmnParam.pageSize)
                        .ToListAsync();
                        recordsTotal = await _ctx.CrmContact.CountAsync();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                //Logs.WriteBug(ex);
            }

            return result = new
            {
                listContact,
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
            CrmContact objContact = null; object result = null;
            try
            {
                using (_ctx = new TestDBContext())
                {
                    objContact = await _ctx.CrmContact.Where(x => x.ContactId == id).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                //Logs.WriteBug(ex);
            }

            return result = new
            {
                objContact
            };

        }


        /// <summary>
        /// Both insert and update can perform by model in database using asynchronous operation. when id is more than 0 update is performed otherwise insert. it returns an object as status of success or failure.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<object> SaveUpdate(VmCrmContact model)
        {
            object result = null; string message = string.Empty; bool resstate = false;
            var objContact = new CrmContact();
            using (_ctx = new TestDBContext())
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (model.ContactId > 0) // update
                        {
                            objContact = await _ctx.CrmContact.Where(x => x.ContactId == model.ContactId).FirstOrDefaultAsync();

                            objContact.ContactName = model.ContactName;
                            objContact.Phone = model.Phone;
                        }
                        else
                        {
                            objContact.ContactName = model.ContactName;
                            objContact.Phone = model.Phone;

                            await _ctx.CrmContact.AddAsync(objContact);
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
                            var delModel = await _ctx.CrmContact.Where(x => x.ContactId == id).FirstOrDefaultAsync();
                            _ctx.CrmContact.Remove(delModel);
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
