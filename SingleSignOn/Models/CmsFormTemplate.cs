//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SingleSignOn.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CmsFormTemplate
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Descript { get; set; }
        public System.Guid Category { get; set; }
        public string Writer { get; set; }
        public Nullable<System.DateTime> WriteDate { get; set; }
        public Nullable<bool> IsSubmit { get; set; }
        public Nullable<System.DateTime> ConfirmDate { get; set; }
        public Nullable<bool> IsApprove { get; set; }
        public string ApproveUid { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public string Reason { get; set; }
        public bool IsAcive { get; set; }
        public bool IsAttachFile { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateUid { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUid { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeleteUid { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public bool Pin { get; set; }
    
        public virtual HrEmpMaster HrEmpMaster { get; set; }
        public virtual HrEmpMaster HrEmpMaster1 { get; set; }
        public virtual HrEmpMaster HrEmpMaster2 { get; set; }
        public virtual HrEmpMaster HrEmpMaster3 { get; set; }
    }
}
