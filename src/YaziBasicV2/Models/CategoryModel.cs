using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;
using YaziBasicV2.Helpers;

namespace YaziBasicV2.Models
{
    public class CategoryModel
    {
        public IEnumerable<CategoryForDisplay> GetCategoryForDisplayDto(IEnumerable<Category> categoryEntity)
        {
            var categoryForDisplayDto = from category in categoryEntity
                                        select new CategoryForDisplay()
                                        {
                                            CategoryId = category.CategoryId,
                                            Description = category.Description,
                                            Name = category.Name,
                                            MetaName = SlugGenerator.GenerateTitleSlug(category.Name),
                                            ArticleType = ((ArticleTypeEnum)category.ArticleTypeId).ToString(),
                                            ImagePath = category.ImagePath
                                        };
            return categoryForDisplayDto;
        }

        public CategoryForDisplay GetCategoryForDisplayDto(Category categoryEntity)
        {
            var categoryForDisplayDto = new CategoryForDisplay()
            {
                CategoryId = categoryEntity.CategoryId,
                Description = categoryEntity.Description,
                Name = categoryEntity.Name,
                MetaName = SlugGenerator.GenerateTitleSlug(categoryEntity.Name),
                ArticleType = ((ArticleTypeEnum)categoryEntity.ArticleTypeId).ToString(),
                ImagePath = categoryEntity.ImagePath
            };
            return categoryForDisplayDto;
        }
    }
}
