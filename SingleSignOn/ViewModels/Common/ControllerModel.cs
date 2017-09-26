using SingleSignOn.Models;
using System;

namespace SingleSignOn.ViewModels
{
    public class ControllerModel
    {
        public string ControllerId { get; set; }
        public string ControllerName { get; set; }
    }

    public class ActionModel
    {
        public string RoleId { get; set; }
        public string ControllerId { get; set; }
        public string ActionId { get; set; }
        public string ActionName { get; set; }
        public bool IsAllow { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUid { get; set; }
        public virtual SysRole SysRole { get; set; }
    }
}