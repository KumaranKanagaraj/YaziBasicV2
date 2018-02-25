using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;
using YaziBasicV2.Models;

namespace YaziBasicV2.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private ArticlesContext _context;

        public CategoryRepository(ArticlesContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category, ArticleTypeEnum articleType)
        {
            category.ArticleTypeId = (int)articleType;
            _context.Category.Add(category);
        }

        public void DeleteCategory(Category category)
        {
            _context.Category.Remove(category);
        }

        public IEnumerable<Category> GetCategoryFromVerity()
        {
            return _context.Category.Where(category => category.ArticleTypeId == (int)ArticleTypeEnum.Verity);
        }

        public IEnumerable<Category> GetCategoryFromEcard()
        {
            return _context.Category.Where(category => category.ArticleTypeId == (int)ArticleTypeEnum.Ecards);
        }

        public Category GetCategoryFromVerity(int categoryId)
        {
            return _context.Category.FirstOrDefault(a => a.CategoryId == categoryId
            && a.ArticleTypeId == (int)ArticleTypeEnum.Verity);
        }

        public Category GetCategoryFromEcard(int categoryId)
        {
            return _context.Category.FirstOrDefault(a => a.CategoryId == categoryId
            && a.ArticleTypeId == (int)ArticleTypeEnum.Ecards);
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Category.FirstOrDefault(a => a.CategoryId == categoryId);
        }

        public Category GetCategory(string categoryName)
        {
            return _context.Category.FirstOrDefault(a => a.Name.ToLower() == categoryName.ToLower());
        }

        public bool IsCategoryExists(int id)
        {
            return _context.Category.Any(a => a.CategoryId == id);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCategory(Category category)
        {
            _context.Category.Update(category);
        }
    }
}
