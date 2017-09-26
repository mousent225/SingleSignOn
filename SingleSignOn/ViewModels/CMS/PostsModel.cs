using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOn.ViewModels
{
    public class PostsModel
    {
        public int PostId { get; set; }
        public string Subject { get; set; }
        public Guid Category { get; set; }
        public string CategoryName { get; set; }
        public string ApplyToPlant { get; set; }
        public string ApplyToPlantName { get; set; }
        public bool IsActive { get; set; }
        public string Content { get; set; }
        public Guid? Kind { get; set; }
        public string KindName { get; set; }
        public int? ReplyToId { get; set; }
        public string CreateUid { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUid { get; set; }
        public string UpdateName { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string DeleteUid { get; set; }
        public Nullable<DateTime> DeleteDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAttachFile { get; set; }
        public int NumRead { get; set; }
    }
}