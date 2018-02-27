using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;

namespace YaziBasicV2.Models
{
    public class EcardModel
    {
        public IEnumerable<EcardForDisplay> GetEcardsForDisplayDto(IEnumerable<Ecards> eCardsEntity,
               IEnumerable<Category> categoryEntity, IEnumerable<SocialImpression> socialImpressionEntity)
        {
            var eCardsForDisplayDto = from eCard in eCardsEntity
                                      join impression in socialImpressionEntity on eCard.Id equals impression.SocialImpressionId
                                      select new EcardForDisplay()
                                      {
                                          Id = eCard.Id,
                                          CategoryName = categoryEntity.Where(id => id.CategoryId == eCard.CategoryId).FirstOrDefault().Name,
                                          AuthorName = ((AuthorEnum)eCard.AuthorId).ToString(),
                                          CreatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", eCard.CreatedTime),
                                          UpdatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", eCard.UpdatedTime),
                                          Description = eCard.Description,
                                          ImagePath = eCard.ImagePath,
                                          IsPublic = eCard.IsPublic,
                                          MetaDescription = eCard.MetaDescription,
                                          Tags = eCard.Tags,
                                          Title = eCard.Title,
                                          MetaTitle = eCard.MetaTitle,
                                          Likes = impression.Likes
                                      };
            return eCardsForDisplayDto;
        }

        public EcardForDisplay GetECardForDisplayDto(Ecards eCardsEntity,
               Category categoryEntity,
               SocialImpression socialImpressionEntity)
        {
            var eCardForDisplayDto = new EcardForDisplay()
            {
                Id = eCardsEntity.Id,
                CategoryName = categoryEntity.Name,
                AuthorName = ((AuthorEnum)eCardsEntity.AuthorId).ToString(),
                CreatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", eCardsEntity.CreatedTime),
                UpdatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", eCardsEntity.UpdatedTime),
                Description = eCardsEntity.Description,
                ImagePath = eCardsEntity.ImagePath,
                IsPublic = eCardsEntity.IsPublic,
                MetaDescription = eCardsEntity.MetaDescription,
                Tags = eCardsEntity.Tags,
                Title = eCardsEntity.Title,
                MetaTitle = eCardsEntity.MetaTitle,
                Likes = socialImpressionEntity.Likes
            };
            return eCardForDisplayDto;
        }

        public IEnumerable<EcardForDisplay> GetECardsForDisplayDto(IEnumerable<Ecards> eCardsEntity, Category category,
                IEnumerable<SocialImpression> socialImpressionEntity)
        {
            var eCardsForDisplayDto = from eCard in eCardsEntity
                                      join impression in socialImpressionEntity on eCard.Id equals impression.SocialImpressionId
                                      select new EcardForDisplay()
                                      {
                                          Id = eCard.Id,
                                          CategoryName = category.Name,
                                          AuthorName = ((AuthorEnum)eCard.AuthorId).ToString(),
                                          CreatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", eCard.CreatedTime),
                                          UpdatedTime = String.Format("{0:MM/dd/yyyy HH:mm:ss}", eCard.UpdatedTime),
                                          Description = eCard.Description,
                                          ImagePath = eCard.ImagePath,
                                          IsPublic = eCard.IsPublic,
                                          MetaDescription = eCard.MetaDescription,
                                          Tags = eCard.Tags,
                                          Title = eCard.Title,
                                          MetaTitle = eCard.MetaTitle,
                                          Likes = impression.Likes
                                      };
            return eCardsForDisplayDto;
        }
    }
}
