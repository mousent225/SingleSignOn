using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOn.ViewModels
{
    public class LogHistoryModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? IpAddress { get; set; }
        public DateTime? LogTime { get; set; }
        public string OperatingSystem { get; set; }
        public string PcName { get; set; }
        public string PcBrowser { get; set; }
        public string StringIpAddress { get; set; }
        public string OrganizeName { get; set; }
        public string PlantName { get; set; }
        public string DeptName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        
        public int CountLogin { get; set; }
    }
}