using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SingleSignOn.ViewModels;
using SingleSignOn.Utilities;
using SingleSignOn.Models;

namespace SingleSignOn.Repositories
{
    public class LogHistoryRepository
    {
        public bool InsertLog(LogHistoryModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var obj = new SysLogHistory
                    {
                        UserId = model.UserId,
                        IpAddress = model.IpAddress,
                        LogTime = DateTime.Now,
                        OperatingSystem = model.OperatingSystem,
                        PcName = model.PcName,
                        PcBrowser = model.PcBrowser,
                        //ControllerName = model.ControllerName,
                        //ActionName = model.ActionName
                    };
                    db.SysLogHistories.Add(obj);
                    db.SaveChanges();

                    return true;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error("Log Repository Insert Log: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        //public List<LogHistoryModel> GetAll()
        //{
        //    try
        //    {
        //        using (var db = new PORTALEntities())
        //        {
        //            var list = (from l in db.SysLogHistories
        //                        join hrem in db.HrEmpMasters on l.UserId equals hrem.Code
        //                        join hrdmf in db.HrDeptMasterFulls on hrem.DeptCode equals hrdmf.Id
        //                        select new LogHistoryModel()
        //                        {
        //                            UserId = l.UserId,
        //                            IpAddress = l.IpAddress,
        //                            LogTime = l.LogTime,
        //                            PcBrowser = l.PcBrowser,
        //                            DeptName = hrdmf.DeptName,
        //                            PlantName = hrdmf.PlantName,
        //                            OrganizeName = hrdmf.OrganizationName,
        //                            SectionName = hrdmf.SectName
        //                        }).ToList();

        //            if (list.Count == 0) return null;
        //            foreach (var item in list)
        //            {
        //                item.StringIpAddress = Util.INT2IP(item.IpAddress ?? 0);
        //            }
        //            return list;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("NoticesRepository GetNotices: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
        //        return null;
        //    }
        //}

        public IEnumerable<LogHistoryModel> GetAll(string subject,int deptId ,DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.SP_SYS_LOG_HISTORY(subject,deptId,dateFrom,dateTo)
                                select new LogHistoryModel()
                                {
                                    UserId = l.UserId,
                                    IpAddress = l.IpAddress,
                                    LogTime = l.LogTime,
                                    DeptName = l.DeptName,
                                    PlantName = l.PlantName,
                                    OrganizeName = l.OrganizationName,
                                    SectionName = l.SectName,
                                    Name = l.LocalName
                                }).ToList();

                    if (list.Count == 0) return null;
                    foreach (var item in list)
                    {
                        item.StringIpAddress = Util.INT2IP(item.IpAddress ?? 0);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("NoticesRepository GetNotices: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<LogHistoryModel>GetByDeptId(int deptCode)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.SP_SYS_GET_LOG_BY_DEPTID(deptCode)
                                select new LogHistoryModel()
                                {
                                    UserId = l.UserId,
                                    IpAddress = l.IpAddress,
                                    LogTime = l.LogTime,
                                    DeptName = l.DeptName,
                                    PlantName = l.PlantName,
                                    OrganizeName = l.OrganizationName,
                                    SectionName = l.SectName,
                                    Name = l.LocalName
                                }).ToList();

                    if (list.Count == 0) return null;
                    foreach (var item in list)
                    {
                        item.StringIpAddress = Util.INT2IP(item.IpAddress ?? 0);
                    }
                    return list;
                }
            }catch(Exception ex)
            {
                LogHelper.Error("NoticesRepository GetNotices: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }
    }
}