using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaziBasicV2.Entities;
using YaziBasicV2.Models;

namespace YaziBasicV2.Services
{
    public class SocialImpressionRepository : ISocialImpressionRepository
    {
        private ArticlesContext _context;

        public SocialImpressionRepository(ArticlesContext context)
        {
            _context = context;
        }

        public void AddImpression(SocialImpression socialImpression)
        {
            //socialImpression.SocialImpressionId = Guid.NewGuid();
            _context.SocialImpression.Add(socialImpression);

        }

        public void DeleteImpression(SocialImpression socialImpression)
        {
            _context.SocialImpression.Remove(socialImpression);
        }

        public SocialImpression GetImpression(Guid socialImpressionId)
        {
            return _context.SocialImpression.Where(a => a.SocialImpressionId == socialImpressionId).FirstOrDefault();
        }

        public IEnumerable<SocialImpression> GetImpressionFromCategory(int categoryId)
        {
            return _context.SocialImpression.Where(a => a.CategoryId == categoryId);
        }

        public IEnumerable<SocialImpression> GetImpressionFromVerity()
        {
            return _context.SocialImpression.Where(a => a.ArticleTypeId == (int)ArticleTypeEnum.Verity);
            //return _context.SocialImpression.ToList();
        }

        public bool IsImpressionExists(Guid socialImpressionId)
        {
            return _context.SocialImpression.Any(a => a.SocialImpressionId == socialImpressionId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateImpression(SocialImpression socialImpression)
        {
            throw new NotImplementedException();
        }
    }
}
