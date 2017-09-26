using System;

namespace SingleSignOn.ViewModels
{
    public class AuthorizationMenuModel
    {
        public Guid ID { get; set; }
        public string MenuID { get; set; }
        public string MasterMenu { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? CreateUID { get; set; }
        public Guid Owner { get; set; }
    }
}