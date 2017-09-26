using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleSignOn.ViewModels;

namespace SingleSignOn.Interfaces
{
    interface IFormTemplateRepository
    {
        IEnumerable<FormTemplateModel> GetAll(string subject, string category, string userId, string creator, int? id, DateTime? dateFrom, DateTime? dateTo);
        //FormTemplateModel GetFormTemplate(int id);
        //IEnumerable<FormTemplateModel> GetFormTemplateViaCate(Guid cate);
        bool InsertFormTemplate(FormTemplateModel model);
        bool UpdateFormTemplate(FormTemplateModel model);
        bool DeleteFormTemplate(FormTemplateModel model);
    }
}
