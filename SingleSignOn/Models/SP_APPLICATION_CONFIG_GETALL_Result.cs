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
    
    public partial class SP_APPLICATION_CONFIG_GETALL_Result
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DeptName { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string ApprovalLineName { get; set; }
        public string ApprovalLineJson { get; set; }
        public string ApprovalLineDefault { get; set; }
        public string CreateUid { get; set; }
        public string CreateName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool Active { get; set; }
        public Nullable<System.Guid> ApprovalKind { get; set; }
        public string KindName { get; set; }
        public string Description { get; set; }
    }
}
