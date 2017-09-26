using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOn.ViewModels
{
    public class AttachFileModel
    {
        public int? AttachId { get; set; }
        public Guid ModuleId { get; set; }
        public int MasterId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}