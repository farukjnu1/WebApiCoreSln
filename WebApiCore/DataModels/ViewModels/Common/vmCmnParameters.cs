using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.ViewModels
{
    public class vmCmnParameters
    {
        #region Common prop
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public bool IsPaging { get; set; }
        public int id { get; set; }
        public int? connectionTypeId { get; set; }
        public int? purposeTypeId { get; set; }
        public int? invId { get; set; }
        public string Name { get; set; }
        public int? ModuleID { get; set; }
        public int? MenuID { get; set; }
        public string userPass { get; set; }
        public string userName { get; set; }
        public string tablename { get; set; }
        public string property { get; set; }
        public string policyTypeId { get; set; }        
        public string values { get; set; }
        public int? CompanyID { get; set; }
        public int? CountryId { get; set; }
        public int? ServiceId { get; set; }
        public int? CloudId { get; set; }
        public int? UserID { get; set; }
        public int? LoggedUserID { get; set; }
        public int? month { get; set; }
        public int? VmNo { get; set; }
        public int? cpu { get; set; }
        public int? memory { get; set; }
        public int? GroupID { get; set; }
        public int? RoleID { get; set; }
        public int? TypeID { get; set; }
        public string ClientIP { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Path { get; set; }
        public bool IsTrue { get; set; }
        public DateTime? SDate { get; set; }
        public DateTime? EDate { get; set; }
        public DateTime? CDate { get; set; }
        public string MailSubject { get; set; }
        public string MailTitle { get; set; }
        public int? TenantId { get; set; }
        public int? PreviousPolicyId { get; set; }
        public int? DeploymentId { get; set; }
        public int? PlanId { get; set; }
        public int? StorageId { get; set; }
        public int? ApprovedNum { get; set; }
        public string vmaction { get; set; }
        public bool Enabled { get; set; }
        public string parentTenantName { get; set; }
        public string parentTenantPassword { get; set; }
        public string Note { get; set; }
        public DateTime? Today { get; set; }       
        public int? userType { get; set; }
        public bool tenantAdmin { get; set; }
        public int subTenantUserId { get; set; }
        public string currentColumn { get; set; }
        public string currentSort { get; set; }
        public string authToken { get; set; }
        public string currency { get; set; }
        public string mrcntNumber { get; set; }
        public string paymentID { get; set; }
        public int? MonthId { get; set; }
        public int? YearId { get; set; }
        public bool? canDeploy { get; set; }
        public bool? canModify { get; set; }
        public int? userStatus { get; set; }
        public decimal? userPrice { get; set; }
        public int? connectionid { get; set; }
        public int? processcid { get; set; }
        public int? accountid { get; set; }
        #endregion

        #region Company Business
        public int? BusinessID { get; set; }
        public decimal? CostPerHour { get; set; }
        public decimal? CostOnetime { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DueAmount { get; set; }
        #endregion

        #region Accounting
        public int GroupHeadID { get; set; }
        public string GroupName { get; set; }
        public int LedgerID { get; set; }
        public string LedgerCode { get; set; }
        #endregion

        #region types
        public string type { get; set; }
        public string subType { get; set; }

        #endregion

        #region Cloud Usage plan
        //public int CloudUsagePlanID { get; set; }

        #endregion

        #region cloud plan
        public int cloudPlanId { get; set; }
        #endregion

        #region Activation Profile
        //public int CloudActivationProfileID { get; set; }
        #endregion

        #region IPTV
        public int Vidid { get; set; }
        public int Catid { get; set; }
        public int subCatid { get; set; }
        public int ProgramId { get; set; }
        #endregion

        #region VM Checking
        public string appId { get; set; }
        public string prevAppId { get; set; }
        public string regionId { get; set; }
        public string memoryname { get; set; }
        public string version { get; set; }
        public string jobId { get; set; }
        public string nics { get; set; }
        public string remoteAccessType { get; set; }
        public string cloudAccountId { get; set; }
        public int? NodeQty { get; set; }
        public bool isTrial { get; set; }
        public string ParentJobName { get; set; }
        public string parentJobStatus { get; set; }
        #endregion

        #region HRM
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
        public int DesignationID { get; set; }
        public int DeviceID { get; set; }
        public int InOutID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? AtnFromDate { get; set; }
        public DateTime? AtnToDate { get; set; }
        public string SrarchKey { get; set; }
        #endregion

        #region BillCloudInvocie
        public int? CloudUserID { get; set; }

        #endregion

        #region BillCloudInvocie
        public int? IspUserID { get; set; }

        #endregion

        #region CRM
        public int CloudTicketCategoryId { get; set; }
        public int CloudTicketId { get; set; }
        #endregion

        #region contact us
       // public string name { get; set; }
        public string email { get; set; }     
        public string phone { get; set; }
        public string message { get; set; }
        #endregion

        #region Billing 
        public decimal rechargeAmount { get; set; }
        #endregion

        #region Email Verification
        public string activationtoken { get; set; }
        #endregion

        public string statusCheckUrl { get; set; }

        #region Business ISP
        public int? topNumber { get; set; }
        #endregion

        public string MenuPath { get; set; }
    }
}
