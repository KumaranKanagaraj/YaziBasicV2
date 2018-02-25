using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;
using YaziBasicV2.Models;

namespace YaziBasicV2.Services
{
    public interface ICategoryRepository
    {
        Category GetCategoryFromVerity(int categoryId);
        Category GetCategoryFromEcard(int categoryId);
        Category GetCategory(int categoryId);
        Category GetCategory(string categoryName);
        void AddCategory(Category category, ArticleTypeEnum articleType);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        bool IsCategoryExists(int id);
        IEnumerable<Category> GetCategoryFromVerity();
        IEnumerable<Category> GetCategoryFromEcard();
        bool Save();

    }
}
