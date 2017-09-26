using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleSignOn.Interfaces
{
    interface ICategoryValueRepository
    {
        IEnumerable<CategoryValueModel> GetListValues(string category, string name, string status);
        IEnumerable<CategoryValueTreeViewModel> GetListViaParent(string category, string parent = "");//, string parent
        IEnumerable<CategoryValueModel> GetActivedValues(string category, bool isEmpty = false);
        CategoryValueModel GetCategoryValue(string id);
        bool InsertCategoryValue(CategoryValueModel model);
        bool UpdateCategoryValue(CategoryValueModel model);
        bool DeleteCategoryValue(CategoryValueModel model);
        bool CheckExistCateValue(string category, string value);
    }
}
