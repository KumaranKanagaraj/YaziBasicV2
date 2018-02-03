using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;

namespace YaziBasicV2.Services
{
    public interface ICategoryRepository
    {
        Category GetCategoryFromVerity(int categoryId);
        Category GetCategory(int categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        bool IsCategoryExists(int id);
        IEnumerable<Category> GetCategoryFromVerity();
        bool Save();

    }
}
