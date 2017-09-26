using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleSignOn.Interfaces
{
    interface ICategoryRepository
    {
        IEnumerable<CategoryModel> GetListCategory(string name);
        CategoryModel GetCategory(string id);
        bool CheckExist(string name);
        bool InsertCategory(CategoryModel model);
        bool UpdateCategory(CategoryModel model);
        bool DeleteCategory(CategoryModel model);
    }
}
