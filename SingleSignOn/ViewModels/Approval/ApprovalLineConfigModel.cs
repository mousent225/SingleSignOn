
using System;

namespace SingleSignOn.Approval.ViewModels
{
    public class ApprovalLineModel
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int ApplicationId { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string DeptFullName { get; set; }
        public string Comment { get; set; }
        public Guid? ApproverType { get; set; }
        public string ApproverTypeName { get; set; }
        public bool? IsView { get; set; }
        public bool? IsApprove { get; set; }
        public DateTime? DateApprove { get; set; }
        public int Seq { get; set; }
    }
    public class ApprovalHistoryModel
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationMasterName { get; set; }
        public int MasterId { get; set; }
        public string ApplicationSubject { get; set; }
        public string ApprovalLine { get; set; }
        public string ApprovalLineJson { get; set; }
        public int CreateUid { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class ApprovalModel
    {
        public int MasterId { get; set; }
        public int ApplicationId { get; set; }
        public bool IsApprove { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public string LinkApplication { get; set; }
    }
}