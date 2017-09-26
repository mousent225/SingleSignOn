using SingleSignOn.Utilities;
using System;

namespace SingleSignOn.Approval.ViewModels
{
    public class ApplicationConfigModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public int? DeptId { get; set; }
        public string DeptName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUid { get; set; }
        public string CreateName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUId { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string ApprovalLineDefault { get; set; }
        public string ApprovalLineName { get; set; }
        public string ApprovalLineJson { get; set; }
        public string DeleteUid { get; set; }
        public Guid? ApprovalKind { get; set; }
        public string KindName { get; set; }
    }

    public class ApplicationConfigTree
    {
        public int id { get; set; }
        public int? parentid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ApplicationMasterModel
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string IdEncrypt { get; set; }
        public string Applicant { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicationName { get; set; }
        public string Subject { get; set; }
        public string Code { get; set; }
        public string System { get; set; }
        public bool Active { get; set; }
        public int? DeptId { get; set; }
        public string DeptName { get; set; }
        public string Description { get; set; }
        public string RequestDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUid { get; set; }
        public string CreateName { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUId { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string ApprovalLine { get; set; }
        public string ApprovalLineName { get; set; }
        public string ApprovalLineJson { get; set; }
        public string DeleteUid { get; set; }
        public Guid? ApprovalKind { get; set; }
        public string KindName { get; set; }
        public Guid ApprovalStatus { get; set; }
        public string ApprovalStatusName { get; set; }
        public string NextApprover { get; set; }
        public string NextApproverName { get; set; }
        public string CostCenter { get; set; }
        public string Opinion { get; set; }
        public string ContactPerson { get; set; }
        public string Content { get; set; }
        public bool IsRecall { get; set; }
    }

    public class SystemRoleModel
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid? Module { get; set; }
        public string ModuleName { get; set; }
        public string EmpIdSameRole { get; set; }
        public string EmpNameSameRole { get; set; }
        public string Remark { get; set; }
    }

    public class EmailRequestModel
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid Request { get; set; }
        public string RequestName { get; set; }
        public string Reason { get; set; }
        public int TimesPerMonth { get; set; }
    }
    public class ItEquipmentModel
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public string ItemName { get; set; }
        public int? Quantity { get; set; }
        public string Operator { get; set; }
        public string Version { get; set; }
        public string Remark { get; set; }
    }
    public class InformationSystemModel
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public Guid System { get; set; }
        public string SystemName { get; set; }
        public Guid Seriousness { get; set; }
        public string SeriousName { get; set; }
        public string Explanation { get; set; }
        public string EmpIp { get; set; }
        public string EmpName { get; set; }
        public int? NumDays { get; set; }
        public string Solution { get; set; }
        public bool IsApprove { get; set; }
    }

    public class NetClientPolicyModel
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public bool IsAllowance { get; set; }
        public string IpAddress { get; set; }
        public string AssetNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Reason { get; set; }
    }

}