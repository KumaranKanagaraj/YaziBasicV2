using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;

namespace YaziBasicV2.Models
{
    public class VerityModel
    {
        public IEnumerable<VerityForDisplayDto> GetVerityForDisplayDto(IEnumerable<Verity> verityEntity,
            IEnumerable<Category> categoryEntity, IEnumerable<SocialImpression> socialImpressionEntity)
        {
            var verityForDisplayDto = from verity in verityEntity
                                      join impression in socialImpressionEntity on verity.VerityId equals impression.SocialImpressionId
                                      select new VerityForDisplayDto()
                                      {
                                          VerityId = verity.VerityId,
                                          CategoryName = categoryEntity.Where(id => id.CategoryId == verity.CategoryId).FirstOrDefault().Name,
                                          AuthorName = ((AuthorEnum)verity.AuthorId).ToString(),
                                          CreatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", verity.CreatedTime),
                                          UpdatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", verity.UpdatedTime),
                                          Description = verity.Description,
                                          ImagePath = verity.ImagePath,
                                          IsPublic = verity.IsPublic,
                                          MetaDescription = verity.MetaDescription,
                                          Tags = verity.Tags,
                                          Title = verity.Title,
                                          MetaTitle = verity.MetaTitle,
                                          Agree = impression.Agree,
                                          DisAgree = impression.DisAgree
                                      };
            return verityForDisplayDto;
        }

        public VerityForDisplayDto GetVerityForDisplayDto(Verity verityEntity,
               Category categoryEntity,
               SocialImpression socialImpressionEntity)
        {
            var verityForDisplayDto = new VerityForDisplayDto()
            {
                VerityId = verityEntity.VerityId,
                CategoryName = categoryEntity.Name,
                AuthorName = ((AuthorEnum)verityEntity.AuthorId).ToString(),
                CreatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", verityEntity.CreatedTime),
                UpdatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", verityEntity.UpdatedTime),
                Description = verityEntity.Description,
                ImagePath = verityEntity.ImagePath,
                IsPublic = verityEntity.IsPublic,
                MetaDescription = verityEntity.MetaDescription,
                Tags = verityEntity.Tags,
                Title = verityEntity.Title,
                MetaTitle = verityEntity.MetaTitle,
                Agree = socialImpressionEntity.Agree,
                DisAgree = socialImpressionEntity.DisAgree
            };
            return verityForDisplayDto;
        }

        public IEnumerable<VerityForDisplayDto> GetVerityForDisplayDto(IEnumerable<Verity> verityEntity, Category category,
                IEnumerable<SocialImpression> socialImpressionEntity)
        {
            var verityForDisplayDto = from verity in verityEntity
                                      join impression in socialImpressionEntity on verity.VerityId equals impression.SocialImpressionId
                                      select new VerityForDisplayDto()
                                      {
                                          VerityId = verity.VerityId,
                                          CategoryName = category.Name,
                                          AuthorName = ((AuthorEnum)verity.AuthorId).ToString(),
                                          CreatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", verity.CreatedTime),
                                          UpdatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", verity.UpdatedTime),
                                          Description = verity.Description,
                                          ImagePath = verity.ImagePath,
                                          IsPublic = verity.IsPublic,
                                          MetaDescription = verity.MetaDescription,
                                          Tags = verity.Tags,
                                          Title = verity.Title,
                                          MetaTitle = verity.MetaTitle,
                                          Agree = impression.Agree,
                                          DisAgree = impression.DisAgree
                                      };
            return verityForDisplayDto;
        }

        public IEnumerable<string> GetVerityTagsByImpression(IEnumerable<Verity> verityEntity,List<Guid> impressionIds)
        {
            var tags = from verity in verityEntity
                       join impression in impressionIds on verity.VerityId equals impression
                       select verity.Tags;
            return tags;
        }

        public IEnumerable<VerityForDisplayDto> GetVerityByTagName(IEnumerable<Verity> verityEntity,
            IEnumerable<SocialImpression> socialImpressionEntity,
            IEnumerable<Category> categoryEntity,
            string searchString)
        {
            var verityForDisplayDto = from verity in verityEntity
                       where verity.Tags.Split(',').Contains(searchString)
                       join impression in socialImpressionEntity on verity.VerityId equals impression.SocialImpressionId
                       select new VerityForDisplayDto()
                       {
                           VerityId = verity.VerityId,
                           CategoryName = categoryEntity.Where(id => id.CategoryId == verity.CategoryId).FirstOrDefault().Name,
                           AuthorName = ((AuthorEnum)verity.AuthorId).ToString(),
                           CreatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", verity.CreatedTime),
                           UpdatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", verity.UpdatedTime),
                           Description = verity.Description,
                           ImagePath = verity.ImagePath,
                           IsPublic = verity.IsPublic,
                           MetaDescription = verity.MetaDescription,
                           Tags = verity.Tags,
                           Title = verity.Title,
                           MetaTitle = verity.MetaTitle,
                           Agree = impression.Agree,
                           DisAgree = impression.DisAgree
                       };

            return verityForDisplayDto;
        }

        public IEnumerable<VerityForDisplayDto> GetVerityByTagName(IEnumerable<Verity> verityEntity,
            IEnumerable<SocialImpression> socialImpressionEntity,
            Category categoryEntity,
            string searchString)
        {
            var verityForDisplayDto = from verity in verityEntity
                                      where verity.Tags.Split(',').Contains(searchString)
                                      join impression in socialImpressionEntity on verity.VerityId equals impression.SocialImpressionId
                                      select new VerityForDisplayDto()
                                      {
                                          VerityId = verity.VerityId,
                                          CategoryName = categoryEntity.Name,
                                          AuthorName = ((AuthorEnum)verity.AuthorId).ToString(),
                                          CreatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", verity.CreatedTime),
                                          UpdatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", verity.UpdatedTime),
                                          Description = verity.Description,
                                          ImagePath = verity.ImagePath,
                                          IsPublic = verity.IsPublic,
                                          MetaDescription = verity.MetaDescription,
                                          Tags = verity.Tags,
                                          Title = verity.Title,
                                          MetaTitle = verity.MetaTitle,
                                          Agree = impression.Agree,
                                          DisAgree = impression.DisAgree
                                      };

            return verityForDisplayDto;
        }
    }
}
