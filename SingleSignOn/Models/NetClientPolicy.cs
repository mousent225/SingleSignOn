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
    
    public partial class NetClientPolicy
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public bool IsAllowance { get; set; }
        public System.DateTime FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> Ip { get; set; }
        public string AssetNo { get; set; }
        public string Reason { get; set; }
    
        public virtual ApplicationMaster ApplicationMaster { get; set; }
    }
}
