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
    
    public partial class SP_APPLICATION_EMAILREQUEST_GETDETAIL_Result
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public string EmpId { get; set; }
        public int DeptId { get; set; }
        public System.Guid Request { get; set; }
        public int TimesPerMonth { get; set; }
        public string Reason { get; set; }
        public string DeptName { get; set; }
        public string EmpName { get; set; }
        public string RequestName { get; set; }
    }
}